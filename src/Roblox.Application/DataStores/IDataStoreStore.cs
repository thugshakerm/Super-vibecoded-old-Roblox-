using Roblox.Domain.DataStores;

namespace Roblox.Application.DataStores;

public interface IDataStoreStore
{
    Task<DataStoreEntry?> FindAsync(long universeId, string storeName, string entryKey, CancellationToken cancellationToken);
}
