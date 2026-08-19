namespace Roblox.Domain.Rendering;

public enum RenderJobState
{
    Queued = 0,
    Leased = 1,
    Completed = 2,
    Failed = 3,
    Cancelled = 4
}
