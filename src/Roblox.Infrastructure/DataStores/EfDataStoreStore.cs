using Microsoft.EntityFrameworkCore;
using Roblox.Application.DataStores;
using Roblox.Domain.DataStores;
using Roblox.Infrastructure.Persistence;

namespace Roblox.Infrastructure.DataStores;

public sealed class EfDataStoreStore(RobloxDbContext dbContext) : IDataStoreStore
{
    public Task<DataStoreEntry?> FindAsync(long universeId, string storeName, string entryKey, CancellationToken cancellationToken) =>
        dbContext.DataStoreEntries.AsNoTracking().SingleOrDefaultAsync(entry =>
            entry.UniverseId == universeId && entry.StoreName == storeName && entry.EntryKey == entryKey,
            cancellationToken);
}
