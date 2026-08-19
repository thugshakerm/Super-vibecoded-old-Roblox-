using Roblox.Domain.Servers;

namespace Roblox.Application.Servers;

public interface ILaunchTicketStore
{
    Task AddAsync(GameLaunchTicket ticket, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    /// <summary>Atomically consumes an unexpired ticket matching the supplied nonce digest.</summary>
    Task<GameLaunchTicket?> TryConsumeAsync(Guid ticketId, string nonceHash, DateTimeOffset now, CancellationToken cancellationToken);
}
