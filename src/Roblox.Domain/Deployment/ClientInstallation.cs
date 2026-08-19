namespace Roblox.Domain.Deployment;
/// <summary>Launcher-local installation status reported by a trusted launcher, never by browser JavaScript.</summary>
public sealed class ClientInstallation
{
 public Guid Id { get; private set; }
 public long UserAccountId { get; private set; }
 public string Channel { get; private set; } = string.Empty;
 public string Version { get; private set; } = string.Empty;
 public ClientInstallState State { get; private set; }
 public DateTimeOffset UpdatedAt { get; private set; }
 private ClientInstallation() { }
}
