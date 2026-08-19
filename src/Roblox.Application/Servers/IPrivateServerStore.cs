using Roblox.Domain.Servers;

namespace Roblox.Application.Servers;

public interface IPrivateServerStore
{
    Task AddAsync(PrivateServer privateServer, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
