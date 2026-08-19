using System.Security.Cryptography;
using Roblox.Application.Servers;

namespace Roblox.Infrastructure.Servers;

public sealed class OpaqueLaunchTicketFactory : ILaunchTicketFactory
{
    public LaunchTicket Create(DateTimeOffset now, TimeSpan lifetime)
    {
        if (lifetime <= TimeSpan.Zero) throw new ArgumentOutOfRangeException(nameof(lifetime));
        var nonce = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32))
            .TrimEnd('=').Replace('+', '-').Replace('/', '_');
        return new LaunchTicket(Guid.NewGuid(), nonce, now.Add(lifetime));
    }

    public string HashNonce(string nonce)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nonce);
        var padded = nonce.Replace('-', '+').Replace('_', '/');
        padded = padded.PadRight(padded.Length + ((4 - padded.Length % 4) % 4), '=');
        return Convert.ToHexString(SHA256.HashData(Convert.FromBase64String(padded)));
    }
}
