namespace Roblox.Application.DataStores;

/// <summary>DataStores are game-server-only; web/browser callers must never satisfy this policy.</summary>
public interface IDataStoreAccessPolicy
{
    bool CanAccessUniverse(long authenticatedServerId, long universeId);
}
