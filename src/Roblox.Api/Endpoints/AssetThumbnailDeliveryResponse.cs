using Roblox.Application.Assets;
using Roblox.Domain.Assets;

namespace Roblox.Api.Endpoints;

/// <summary>Public delivery state. Storage keys are intentionally never serialized to browser clients.</summary>
internal sealed record AssetThumbnailDeliveryResponse(
    long AssetId,
    ThumbnailState State,
    AssetDeliveryErrorCode ErrorCode,
    bool Retryable,
    int? RetryAfterSeconds)
{
    public static AssetThumbnailDeliveryResponse From(AssetDeliveryResult result) => new(
        result.AssetId,
        result.State,
        result.ErrorCode,
        result.Retryable,
        result.RetryAfter is { } retryAfter ? (int)Math.Ceiling(retryAfter.TotalSeconds) : null);
}
