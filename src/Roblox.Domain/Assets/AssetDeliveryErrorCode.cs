namespace Roblox.Domain.Assets;

/// <summary>Stable reasons used to select a historically correct delivery fallback.</summary>
public enum AssetDeliveryErrorCode
{
    None = 0,
    AssetOwnerNoLongerOwnsItem,
    AssetOwnerDeletedItem,
    PlaceThumbnailUnavailable,
    AvatarThumbnailPending,
    AvatarThumbnailUnavailable,
    AssetThumbnailPending,
    AssetThumbnailUnavailable,
    AssetModerated,
    AssetPrivate,
    AssetNotFound,
    GenericContentFallback
}
