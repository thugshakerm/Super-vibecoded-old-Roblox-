namespace Roblox.Domain.Servers;
/// <summary>Private control-plane record. Endpoint/address data is never returned to browsers.</summary>
public sealed class GameServer
{
 public Guid Id { get; private set; }
 public long PlaceId { get; private set; }
 public Guid? PrivateServerId { get; private set; }
 public GameServerState State { get; private set; }
 public int PlayerCount { get; private set; }
 public int MaxPlayers { get; private set; }
 public DateTimeOffset LeaseExpiresAt { get; private set; }
 private GameServer() { }
}
