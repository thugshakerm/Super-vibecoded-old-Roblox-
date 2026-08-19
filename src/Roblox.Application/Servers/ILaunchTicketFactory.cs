namespace Roblox.Application.Servers;

public interface ILaunchTicketFactory
{
    LaunchTicket Create(DateTimeOffset now, TimeSpan lifetime);
    string HashNonce(string nonce);
}
