namespace Roblox.Domain.Assets;

/// <summary>Immutable object-storage pointer for one approved or pending asset upload.</summary>
public sealed class AssetVersion
{
    public long Id { get; private set; }
    public long AssetId { get; private set; }
    public int VersionNumber { get; private set; }
    public string ObjectKey { get; private set; } = string.Empty;
    public string ContentType { get; private set; } = string.Empty;
    public long ContentLength { get; private set; }
    public string Sha256 { get; private set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; private set; }

    private AssetVersion() { }
}
