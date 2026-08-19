namespace Roblox.Domain.Servers;
/// <summary>Immutable non-secret event for diagnosing launch state without retaining tokens or endpoints.</summary>
public sealed class LaunchAuditEvent
{
 public long Id { get; private set; }
 public Guid LaunchTicketId { get; private set; }
 public string EventCode { get; private set; } = string.Empty;
 public DateTimeOffset CreatedAt { get; private set; }
 private LaunchAuditEvent() { }
}
