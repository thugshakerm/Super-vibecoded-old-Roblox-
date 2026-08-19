namespace Roblox.Domain.Deployment;

/// <summary>Approved client release selection for a historical client channel.</summary>
public sealed class ClientRelease
{
    public long Id { get; private set; }
    public string Channel { get; private set; } = string.Empty;
    public string Version { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public List<ClientPackage> Packages { get; } = [];

    private ClientRelease() { }
}
