namespace Roblox.Application.Identity;

/// <summary>Opaque raw value returned only to the authentication boundary; it is never persisted.</summary>
public sealed record SessionToken(string Value, string Hash, DateTimeOffset ExpiresAt);
