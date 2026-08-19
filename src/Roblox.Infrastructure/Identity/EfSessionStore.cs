using Microsoft.EntityFrameworkCore;
using Roblox.Application.Identity;
using Roblox.Domain.Identity;
using Roblox.Infrastructure.Persistence;

namespace Roblox.Infrastructure.Identity;

public sealed class EfSessionStore(RobloxDbContext dbContext) : ISessionStore
{
    public Task AddAsync(UserSession userSession, CancellationToken cancellationToken) =>
        dbContext.UserSessions.AddAsync(userSession, cancellationToken).AsTask();

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        dbContext.SaveChangesAsync(cancellationToken);

    public async Task<AuthenticatedSession?> FindActiveAsync(
        string tokenHash,
        DateTimeOffset now,
        CancellationToken cancellationToken)
    {
        var session = await dbContext.UserSessions
            .AsNoTracking()
            .SingleOrDefaultAsync(candidate =>
                candidate.TokenHash == tokenHash &&
                candidate.RevokedAt == null &&
                candidate.ExpiresAt > now,
                cancellationToken);

        return session is null
            ? null
            : new AuthenticatedSession(session.UserAccountId, session.Id, session.ExpiresAt);
    }

    public async Task RevokeAsync(string tokenHash, DateTimeOffset now, CancellationToken cancellationToken)
    {
        var session = await dbContext.UserSessions
            .SingleOrDefaultAsync(candidate => candidate.TokenHash == tokenHash, cancellationToken);
        if (session is null) return;

        session.Revoke(now);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
