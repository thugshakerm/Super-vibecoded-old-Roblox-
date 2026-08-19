namespace Roblox.Domain.Identity;

/// <summary>
/// Account lifecycle state. Authorization policy will interpret these states in a later step.
/// </summary>
public enum UserAccountState
{
    Active = 0,
    UnderReview = 1,
    Banned = 2,
    Deleted = 3
}
