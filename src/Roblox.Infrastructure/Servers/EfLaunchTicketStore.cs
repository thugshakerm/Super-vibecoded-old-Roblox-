using Microsoft.EntityFrameworkCore;
using Roblox.Application.Servers;
using Roblox.Domain.Servers;
using Roblox.Infrastructure.Persistence;

namespace Roblox.Infrastructure.Servers;

public sealed class EfLaunchTicketStore(RobloxDbContext dbContext) : ILaunchTicketStore
{
    public Task AddAsync(GameLaunchTicket ticket, CancellationToken cancellationToken) =>
        dbContext.GameLaunchTickets.AddAsync(ticket, cancellationToken).AsTask();

    public Task SaveChangesAsync(CancellationToken cancellationToken) => dbContext.SaveChangesAsync(cancellationToken);

    public async Task<GameLaunchTicket?> TryConsumeAsync(
        Guid ticketId,
        string nonceHash,
        DateTimeOffset now,
        CancellationToken cancellationToken)
    {
        // One conditional UPDATE prevents two server processes from consuming one ticket.
        var changed = await dbContext.GameLaunchTickets
            .Where(candidate =>
                candidate.Id == ticketId &&
                candidate.NonceHash == nonceHash &&
                candidate.ConsumedAt == null &&
                candidate.ExpiresAt > now)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(ticket => ticket.ConsumedAt, now), cancellationToken);

        if (changed != 1) return null;

        return await dbContext.GameLaunchTickets
            .AsNoTracking()
            .SingleAsync(candidate => candidate.Id == ticketId, cancellationToken);
    }

}
