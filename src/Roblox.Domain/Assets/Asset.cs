namespace Roblox.Domain.Assets;

/// <summary>
/// Metadata only. Original files and generated thumbnails are never stored in PostgreSQL.
/// </summary>
public sealed class Asset
{
    public long Id { get; private set; }
    public AssetType Type { get; private set; }
    public long CreatorUserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public AssetModerationState ModerationState { get; private set; }
    public bool IsPublic { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }
    public List<AssetVersion> Versions { get; } = [];

    private Asset() { }

    public Asset(long id, AssetType type, long creatorUserId, string name, DateTimeOffset now)
    {
        if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
        if (creatorUserId <= 0) throw new ArgumentOutOfRangeException(nameof(creatorUserId));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("An asset name is required.", nameof(name));

        Id = id;
        Type = type;
        CreatorUserId = creatorUserId;
        Name = name.Trim();
        ModerationState = AssetModerationState.Pending;
        CreatedAt = now;
        UpdatedAt = now;
    }
}
