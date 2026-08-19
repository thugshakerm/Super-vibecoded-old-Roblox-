using System.Security.Cryptography;
using Roblox.Application.Identity;

namespace Roblox.Infrastructure.Identity;

public sealed class OpaqueSessionTokenFactory : ISessionTokenFactory
{
    private const int TokenSizeBytes = 32;

    public SessionToken Create(DateTimeOffset now, TimeSpan lifetime)
    {
        if (lifetime <= TimeSpan.Zero) throw new ArgumentOutOfRangeException(nameof(lifetime));

        var bytes = RandomNumberGenerator.GetBytes(TokenSizeBytes);
        var rawToken = Convert.ToBase64String(bytes)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
        var tokenHash = Convert.ToHexString(SHA256.HashData(bytes));
        return new SessionToken(rawToken, tokenHash, now.Add(lifetime));
    }

    public string Hash(string rawToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(rawToken);
        var paddedToken = rawToken.Replace('-', '+').Replace('_', '/');
        paddedToken = paddedToken.PadRight(paddedToken.Length + ((4 - paddedToken.Length % 4) % 4), '=');
        var bytes = Convert.FromBase64String(paddedToken);
        return Convert.ToHexString(SHA256.HashData(bytes));
    }
}
