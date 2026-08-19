namespace Roblox.Application.Identity;

public sealed record AuthenticatedSession(long UserId, Guid SessionId, DateTimeOffset ExpiresAt);
