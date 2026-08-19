using Roblox.Domain.Servers;

namespace Roblox.Application.Servers;

public interface IPrivateServerLinkStore
{
    Task AddAsync(PrivateServerLink link, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
