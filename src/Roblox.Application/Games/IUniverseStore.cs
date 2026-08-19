using Roblox.Domain.Games;

namespace Roblox.Application.Games;

public interface IUniverseStore
{
    Task AddAsync(Universe universe, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
