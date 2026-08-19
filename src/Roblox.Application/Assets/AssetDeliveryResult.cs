using Roblox.Domain.Assets;

namespace Roblox.Application.Assets;

public sealed record AssetDeliveryResult(
    long AssetId,
    ThumbnailState State,
    AssetDeliveryErrorCode ErrorCode,
    string? ObjectKey,
    bool Retryable,
    TimeSpan? RetryAfter);
