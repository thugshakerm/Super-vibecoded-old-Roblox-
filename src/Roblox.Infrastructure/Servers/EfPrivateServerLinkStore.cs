using Roblox.Application.Servers;
using Roblox.Domain.Servers;
using Roblox.Infrastructure.Persistence;

namespace Roblox.Infrastructure.Servers;

public sealed class EfPrivateServerLinkStore(RobloxDbContext dbContext) : IPrivateServerLinkStore
{
    public Task AddAsync(PrivateServerLink link, CancellationToken cancellationToken) =>
        dbContext.PrivateServerLinks.AddAsync(link, cancellationToken).AsTask();

    public Task SaveChangesAsync(CancellationToken cancellationToken) => dbContext.SaveChangesAsync(cancellationToken);
}
