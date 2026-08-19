using Roblox.Domain.Assets;

namespace Roblox.Application.Assets;

public static class AssetObjectKeyFactory
{
    public static ObjectKey Original(long assetId, int version) =>
        ObjectKey.Create($"assets/{assetId}/versions/{version}/original");

    public static ObjectKey Thumbnail(long assetId, int version, int width, int height) =>
        ObjectKey.Create($"thumbnails/assets/{assetId}/versions/{version}/{width}x{height}.png");
}
