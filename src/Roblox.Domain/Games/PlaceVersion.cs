namespace Roblox.Domain.Games;

/// <summary>Immutable metadata for a place file held in private object storage.</summary>
public sealed class PlaceVersion
{
    public long Id { get; private set; }
    public long PlaceId { get; private set; }
    public int VersionNumber { get; private set; }
    public string ObjectKey { get; private set; } = string.Empty;
    public string Sha256 { get; private set; } = string.Empty;
    public long ContentLength { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    private PlaceVersion() { }
}
