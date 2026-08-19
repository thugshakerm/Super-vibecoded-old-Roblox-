namespace Roblox.Application.Assets;

public interface IAssetDeliveryService
{
    Task<AssetDeliveryResult> GetThumbnailDeliveryAsync(
        long assetId,
        int width,
        int height,
        CancellationToken cancellationToken);
}
