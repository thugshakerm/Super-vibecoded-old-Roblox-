namespace Roblox.Application.Identity;

public sealed record SignInResult(AccountResultCode Code, long? UserId, SessionToken? Session)
{
    public bool Succeeded => Code == AccountResultCode.Success;
}
