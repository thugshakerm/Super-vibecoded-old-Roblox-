namespace Roblox.Domain.Identity;

/// <summary>
/// Server-side record for an opaque authenticated session token. Only its SHA-256 digest is persisted.
/// </summary>
public sealed class UserSession
{
    public Guid Id { get; private set; }
    public long UserAccountId { get; private set; }
    public string TokenHash { get; private set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset ExpiresAt { get; private set; }
    public DateTimeOffset? RevokedAt { get; private set; }

    private UserSession() { }

    public UserSession(long userAccountId, string tokenHash, DateTimeOffset createdAt, DateTimeOffset expiresAt)
    {
        if (userAccountId <= 0) throw new ArgumentOutOfRangeException(nameof(userAccountId));
        if (string.IsNullOrWhiteSpace(tokenHash)) throw new ArgumentException("A token hash is required.", nameof(tokenHash));
        if (expiresAt <= createdAt) throw new ArgumentOutOfRangeException(nameof(expiresAt));

        Id = Guid.NewGuid();
        UserAccountId = userAccountId;
        TokenHash = tokenHash;
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
    }

    public void Revoke(DateTimeOffset now)
    {
        if (RevokedAt is null)
        {
            RevokedAt = now;
        }
    }
}
