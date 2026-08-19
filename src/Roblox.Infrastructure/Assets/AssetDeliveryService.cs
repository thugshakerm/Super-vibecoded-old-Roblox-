using Microsoft.EntityFrameworkCore;
using Roblox.Application.Assets;
using Roblox.Domain.Assets;
using Roblox.Infrastructure.Persistence;

namespace Roblox.Infrastructure.Assets;

/// <summary>
/// Resolves a thumbnail state without exposing object-storage paths or falling back to a browser error.
/// Website-specific image URLs are deliberately resolved above this layer.
/// </summary>
public sealed class AssetDeliveryService(RobloxDbContext dbContext) : IAssetDeliveryService
{
    public async Task<AssetDeliveryResult> GetThumbnailDeliveryAsync(
        long assetId,
        int width,
        int height,
        CancellationToken cancellationToken)
    {
        if (assetId <= 0 || width <= 0 || height <= 0)
        {
            return Unavailable(assetId, AssetDeliveryErrorCode.AssetNotFound);
        }

        var asset = await dbContext.Assets
            .AsNoTracking()
            .SingleOrDefaultAsync(candidate => candidate.Id == assetId, cancellationToken);

        if (asset is null)
        {
            return Unavailable(assetId, AssetDeliveryErrorCode.AssetNotFound);
        }

        if (!asset.IsPublic)
        {
            return Unavailable(assetId, AssetDeliveryErrorCode.AssetPrivate);
        }

        if (asset.ModerationState is AssetModerationState.Rejected or AssetModerationState.Quarantined)
        {
            return Unavailable(assetId, AssetDeliveryErrorCode.AssetModerated);
        }

        if (asset.ModerationState == AssetModerationState.Deleted)
        {
            return Unavailable(assetId, AssetDeliveryErrorCode.AssetOwnerDeletedItem);
        }

        var thumbnail = await dbContext.Thumbnails
            .AsNoTracking()
            .SingleOrDefaultAsync(candidate =>
                candidate.AssetId == assetId && candidate.Width == width && candidate.Height == height,
                cancellationToken);

        if (thumbnail is null || thumbnail.State == ThumbnailState.Pending)
        {
            return new AssetDeliveryResult(
                assetId,
                ThumbnailState.Pending,
                AssetDeliveryErrorCode.AssetThumbnailPending,
                null,
                true,
                TimeSpan.FromSeconds(3));
        }

        if (thumbnail.State == ThumbnailState.Available && !string.IsNullOrWhiteSpace(thumbnail.ObjectKey))
        {
            return new AssetDeliveryResult(
                assetId,
                ThumbnailState.Available,
                AssetDeliveryErrorCode.None,
                thumbnail.ObjectKey,
                false,
                null);
        }

        return Unavailable(assetId, thumbnail.ErrorCode == AssetDeliveryErrorCode.None
            ? AssetDeliveryErrorCode.AssetThumbnailUnavailable
            : thumbnail.ErrorCode);
    }

    private static AssetDeliveryResult Unavailable(long assetId, AssetDeliveryErrorCode errorCode) =>
        new(assetId, ThumbnailState.Unavailable, errorCode, null, false, null);
}
