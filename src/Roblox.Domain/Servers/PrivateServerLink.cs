namespace Roblox.Domain.Servers;

/// <summary>
/// Shareable private-server link representation. Only the SHA-256 code digest is persisted.
/// </summary>
public sealed class PrivateServerLink
{
    public Guid Id { get; private set; }
    public Guid PrivateServerId { get; private set; }
    public string CodeHash { get; private set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? ExpiresAt { get; private set; }
    public DateTimeOffset? RevokedAt { get; private set; }

    private PrivateServerLink() { }

    public PrivateServerLink(Guid privateServerId, string codeHash, DateTimeOffset createdAt, DateTimeOffset? expiresAt)
    {
        if (privateServerId == Guid.Empty) throw new ArgumentOutOfRangeException(nameof(privateServerId));
        if (string.IsNullOrWhiteSpace(codeHash)) throw new ArgumentException("A link code hash is required.", nameof(codeHash));
        if (expiresAt is not null && expiresAt <= createdAt) throw new ArgumentOutOfRangeException(nameof(expiresAt));

        Id = Guid.NewGuid();
        PrivateServerId = privateServerId;
        CodeHash = codeHash;
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
    }
}
