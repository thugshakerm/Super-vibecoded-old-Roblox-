namespace Roblox.Domain.Rendering;

/// <summary>
/// Render output is selected by the source page's historically documented slot.
/// AvatarHeadshot is close-up only; it is never substituted for a full-body slot.
/// </summary>
public enum RenderJobKind
{
    AvatarFullBody = 1,
    AvatarHeadshot = 2,
    AssetThumbnail = 3,
    PlaceThumbnail = 4
}
