namespace Roblox.Domain.Deployment;

/// <summary>One immutable, hash-verified file required by a client release.</summary>
public sealed class ClientPackage
{
    public long Id { get; private set; }
    public long ClientReleaseId { get; private set; }
    public string Version { get; private set; } = string.Empty;
    public string FileName { get; private set; } = string.Empty;
    public string ObjectKey { get; private set; } = string.Empty;
    public string Sha256 { get; private set; } = string.Empty;
    public long ContentLength { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    private ClientPackage() { }
}
