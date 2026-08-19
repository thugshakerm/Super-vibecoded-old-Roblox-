namespace Roblox.Domain.Games;

/// <summary>A creator-owned game container. A universe can contain one or more places.</summary>
public sealed class Universe
{
    public long Id { get; private set; }
    public long CreatorUserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }
    public List<Place> Places { get; } = [];

    private Universe() { }

    public Universe(long creatorUserId, string name, DateTimeOffset now)
    {
        if (creatorUserId <= 0) throw new ArgumentOutOfRangeException(nameof(creatorUserId));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("A universe name is required.", nameof(name));

        CreatorUserId = creatorUserId;
        Name = name.Trim();
        CreatedAt = now;
        UpdatedAt = now;
    }
}
