namespace Roblox.Domain.DataStores;

/// <summary>Versioned server-only value for one universe-scoped DataStore key.</summary>
public sealed class DataStoreEntry
{
    public long Id { get; private set; }
    public long UniverseId { get; private set; }
    public string StoreName { get; private set; } = string.Empty;
    public string EntryKey { get; private set; } = string.Empty;
    public string JsonValue { get; private set; } = string.Empty;
    public long Version { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    private DataStoreEntry() { }
}
