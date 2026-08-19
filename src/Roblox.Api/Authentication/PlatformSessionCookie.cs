namespace Roblox.Api.Authentication;

internal static class PlatformSessionCookie
{
    // A project-owned cookie name avoids impersonating historical production credentials.
    public const string Name = "rbx_session";
}
