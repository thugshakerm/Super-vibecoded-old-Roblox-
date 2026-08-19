namespace Roblox.Domain.Identity;

/// <summary>
/// Credential metadata. Password plaintext is never represented or persisted after hashing.
/// </summary>
public sealed class PasswordCredential
{
    public long UserAccountId { get; private set; }
    public string Algorithm { get; private set; } = string.Empty;
    public int WorkFactor { get; private set; }
    public string Salt { get; private set; } = string.Empty;
    public string Hash { get; private set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    private PasswordCredential() { }

    internal PasswordCredential(
        string algorithm,
        int workFactor,
        string salt,
        string hash,
        DateTimeOffset now)
    {
        if (string.IsNullOrWhiteSpace(algorithm)) throw new ArgumentException("An algorithm is required.", nameof(algorithm));
        if (workFactor <= 0) throw new ArgumentOutOfRangeException(nameof(workFactor));
        if (string.IsNullOrWhiteSpace(salt)) throw new ArgumentException("A salt is required.", nameof(salt));
        if (string.IsNullOrWhiteSpace(hash)) throw new ArgumentException("A hash is required.", nameof(hash));

        Algorithm = algorithm;
        WorkFactor = workFactor;
        Salt = salt;
        Hash = hash;
        CreatedAt = now;
        UpdatedAt = now;
    }
}
