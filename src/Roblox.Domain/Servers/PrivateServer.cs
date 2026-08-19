namespace Roblox.Domain.Servers;

/// <summary>Persistent owner/access record; it is not a running game-server process.</summary>
public sealed class PrivateServer
{
    public Guid Id { get; private set; }
    public long UniverseId { get; private set; }
    public long OwnerUserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public PrivateServerState State { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? ExpiresAt { get; private set; }

    private PrivateServer() { }

    public PrivateServer(long universeId, long ownerUserId, string name, DateTimeOffset now, DateTimeOffset? expiresAt)
    {
        if (universeId <= 0) throw new ArgumentOutOfRangeException(nameof(universeId));
        if (ownerUserId <= 0) throw new ArgumentOutOfRangeException(nameof(ownerUserId));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("A private server name is required.", nameof(name));
        if (expiresAt is not null && expiresAt <= now) throw new ArgumentOutOfRangeException(nameof(expiresAt));

        Id = Guid.NewGuid();
        UniverseId = universeId;
        OwnerUserId = ownerUserId;
        Name = name.Trim();
        State = PrivateServerState.Active;
        CreatedAt = now;
        ExpiresAt = expiresAt;
    }
}
