using Roblox.Application.Servers;
using Roblox.Domain.Servers;
using Roblox.Infrastructure.Persistence;

namespace Roblox.Infrastructure.Servers;

public sealed class EfPrivateServerStore(RobloxDbContext dbContext) : IPrivateServerStore
{
    public Task AddAsync(PrivateServer privateServer, CancellationToken cancellationToken) =>
        dbContext.PrivateServers.AddAsync(privateServer, cancellationToken).AsTask();

    public Task SaveChangesAsync(CancellationToken cancellationToken) => dbContext.SaveChangesAsync(cancellationToken);
}
