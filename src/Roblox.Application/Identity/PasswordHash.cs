namespace Roblox.Application.Identity;

/// <summary>Opaque password-hash payload suitable for persistence. Never log it.</summary>
public sealed record PasswordHash(string Algorithm, int WorkFactor, string Salt, string Hash);
