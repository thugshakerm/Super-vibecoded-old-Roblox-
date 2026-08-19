using System.Security.Cryptography;
using Roblox.Application.Identity;

namespace Roblox.Infrastructure.Identity;

/// <summary>
/// Versioned PBKDF2-SHA512 hashing using a per-password random salt and constant-time verification.
/// The work factor is stored with the credential so it can be upgraded on a later successful login.
/// </summary>
public sealed class Pbkdf2PasswordHasher : IPasswordHasher
{
    public const string AlgorithmName = "PBKDF2-SHA512";
    private const int SaltSizeBytes = 16;
    private const int HashSizeBytes = 32;
    private const int Iterations = 600_000;

    public PasswordHash Hash(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        var salt = RandomNumberGenerator.GetBytes(SaltSizeBytes);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            Iterations,
            HashAlgorithmName.SHA512,
            HashSizeBytes);

        return new PasswordHash(
            AlgorithmName,
            Iterations,
            Convert.ToBase64String(salt),
            Convert.ToBase64String(hash));
    }

    public bool Verify(string password, PasswordHash passwordHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        ArgumentNullException.ThrowIfNull(passwordHash);

        if (!string.Equals(passwordHash.Algorithm, AlgorithmName, StringComparison.Ordinal) ||
            passwordHash.WorkFactor < 100_000 ||
            passwordHash.WorkFactor > 5_000_000)
        {
            return false;
        }

        try
        {
            var salt = Convert.FromBase64String(passwordHash.Salt);
            var expectedHash = Convert.FromBase64String(passwordHash.Hash);
            if (salt.Length < SaltSizeBytes || expectedHash.Length != HashSizeBytes)
            {
                return false;
            }

            var actualHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                passwordHash.WorkFactor,
                HashAlgorithmName.SHA512,
                HashSizeBytes);

            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
