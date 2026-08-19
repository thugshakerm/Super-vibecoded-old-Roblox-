namespace Roblox.Application.Servers;

/// <summary>Raw launch nonce is returned only to a future trusted launcher boundary.</summary>
public sealed record LaunchTicket(Guid TicketId, string Nonce, DateTimeOffset ExpiresAt);
