using Roblox.Application.Games;
using Roblox.Domain.Games;
using Roblox.Infrastructure.Persistence;

namespace Roblox.Infrastructure.Games;

public sealed class EfUniverseStore(RobloxDbContext dbContext) : IUniverseStore
{
    public Task AddAsync(Universe universe, CancellationToken cancellationToken) =>
        dbContext.Universes.AddAsync(universe, cancellationToken).AsTask();

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        dbContext.SaveChangesAsync(cancellationToken);
}
