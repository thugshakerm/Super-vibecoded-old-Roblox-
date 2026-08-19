using System.Security.Cryptography;
using Roblox.Application.Servers;

namespace Roblox.Infrastructure.Servers;

/// <summary>
/// C# compatibility implementation of the supplied PHP GenerateLinkCodeCrypto algorithm:
/// ceil(length / 4) * 3 random bytes, base64, exact-length slice, then '+'/'/' URL-safe replacement.
/// </summary>
public sealed class VipServerLinkCodeGenerator : IVipServerLinkCodeGenerator
{
    public string Generate(int length)
    {
        if (length <= 0 || length > 256) throw new ArgumentOutOfRangeException(nameof(length));

        var byteCount = checked(((length + 3) / 4) * 3);
        var randomBytes = RandomNumberGenerator.GetBytes(byteCount);
        var base64 = Convert.ToBase64String(randomBytes);
        return base64[..length].Replace('+', '-').Replace('/', '_');
    }

    public string Hash(string code)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        return Convert.ToHexString(SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(code)));
    }
}
