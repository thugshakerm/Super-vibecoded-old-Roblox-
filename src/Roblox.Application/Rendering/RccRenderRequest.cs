using Roblox.Domain.Rendering;

namespace Roblox.Application.Rendering;

/// <summary>Trusted render parameters produced by application code, not arbitrary client script.</summary>
public sealed record RccRenderRequest(
    Guid RenderJobId,
    RenderJobKind Kind,
    long TargetId,
    long AppearanceOrAssetVersion,
    int Width,
    int Height);
