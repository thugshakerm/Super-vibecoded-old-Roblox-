namespace Roblox.Domain.Games;

/// <summary>A launchable place within a universe. Place content is immutable by version.</summary>
public sealed class Place
{
    public long Id { get; private set; }
    public long UniverseId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public PlaceAccessType AccessType { get; private set; }
    public bool IsRootPlace { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }
    public List<PlaceVersion> Versions { get; } = [];

    private Place() { }
}
