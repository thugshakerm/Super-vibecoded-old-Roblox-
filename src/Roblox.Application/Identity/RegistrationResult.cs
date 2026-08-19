namespace Roblox.Application.Identity;

public sealed record RegistrationResult(AccountResultCode Code, long? UserId)
{
    public bool Succeeded => Code == AccountResultCode.Success;
}
