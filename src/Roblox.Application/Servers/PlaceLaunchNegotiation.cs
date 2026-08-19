namespace Roblox.Application.Servers;
/// <summary>Private launcher response; never rendered into HTML or exposed as a broad account token.</summary>
public sealed record PlaceLaunchNegotiation(Guid LauncherAuthorizationId, string LauncherToken, DateTimeOffset ExpiresAt);
