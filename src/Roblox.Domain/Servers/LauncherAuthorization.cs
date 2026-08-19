namespace Roblox.Domain.Servers;
/// <summary>One-time launcher handoff authorization; raw token is never persisted.</summary>
public sealed class LauncherAuthorization
{
 public Guid Id { get; private set; }
 public long UserAccountId { get; private set; }
 public Guid LaunchTicketId { get; private set; }
 public string TokenHash { get; private set; } = string.Empty;
 public DateTimeOffset ExpiresAt { get; private set; }
 public DateTimeOffset? RedeemedAt { get; private set; }
 private LauncherAuthorization() { }
}
