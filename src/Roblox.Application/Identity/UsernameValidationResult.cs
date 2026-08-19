namespace Roblox.Application.Identity;

public sealed record UsernameValidationResult(bool IsValid, string? FailureReason)
{
    public static readonly UsernameValidationResult Valid = new(true, null);
}
