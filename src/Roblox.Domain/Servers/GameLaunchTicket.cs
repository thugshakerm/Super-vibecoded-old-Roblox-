namespace Roblox.Domain.Servers;

/// <summary>Single-use server-side launch authorization. Only the nonce digest is persisted.</summary>
public sealed class GameLaunchTicket
{
    public Guid Id { get; private set; }
    public long UserAccountId { get; private set; }
    public long PlaceId { get; private set; }
    public Guid? PrivateServerId { get; private set; }
    public string NonceHash { get; private set; } = string.Empty;
    public DateTimeOffset IssuedAt { get; private set; }
    public DateTimeOffset ExpiresAt { get; private set; }
    public DateTimeOffset? ConsumedAt { get; private set; }

    private GameLaunchTicket() { }

    public GameLaunchTicket(long userAccountId, long placeId, Guid? privateServerId, string nonceHash, DateTimeOffset issuedAt, DateTimeOffset expiresAt)
    {
        if (userAccountId <= 0 || placeId <= 0) throw new ArgumentOutOfRangeException(nameof(placeId));
        if (string.IsNullOrWhiteSpace(nonceHash)) throw new ArgumentException("A nonce hash is required.", nameof(nonceHash));
        if (expiresAt <= issuedAt) throw new ArgumentOutOfRangeException(nameof(expiresAt));

        Id = Guid.NewGuid();
        UserAccountId = userAccountId;
        PlaceId = placeId;
        PrivateServerId = privateServerId;
        NonceHash = nonceHash;
        IssuedAt = issuedAt;
        ExpiresAt = expiresAt;
    }

    public void Consume(DateTimeOffset now)
    {
        if (ConsumedAt is not null) throw new InvalidOperationException("Launch ticket is already consumed.");
        if (now >= ExpiresAt) throw new InvalidOperationException("Launch ticket has expired.");
        ConsumedAt = now;
    }
}
