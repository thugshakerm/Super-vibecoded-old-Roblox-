namespace Roblox.Domain.Assets;

/// <summary>Generated thumbnail metadata. The rendered image itself lives in object storage.</summary>
public sealed class Thumbnail
{
    public long Id { get; private set; }
    public long AssetId { get; private set; }
    public ThumbnailState State { get; private set; }
    public AssetDeliveryErrorCode ErrorCode { get; private set; }
    public string? ObjectKey { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    private Thumbnail() { }
}
