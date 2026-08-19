using Roblox.Domain.Assets;

namespace Roblox.Application.Assets;

/// <summary>
/// A presentation-neutral result. The website will map each code to the verified
/// period asset for the requested page era; it must not use a generic browser fallback.
/// </summary>
public sealed record PlaceholderResolution(
    AssetDeliveryErrorCode ErrorCode,
    bool Retryable,
    TimeSpan? RetryAfter);
