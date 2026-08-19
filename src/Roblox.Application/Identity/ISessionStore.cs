using Roblox.Domain.Identity;

namespace Roblox.Application.Identity;

public interface ISessionStore
{
    Task AddAsync(UserSession userSession, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<AuthenticatedSession?> FindActiveAsync(string tokenHash, DateTimeOffset now, CancellationToken cancellationToken);
    Task RevokeAsync(string tokenHash, DateTimeOffset now, CancellationToken cancellationToken);
}
