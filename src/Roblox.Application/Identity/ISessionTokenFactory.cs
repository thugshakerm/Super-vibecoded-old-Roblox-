namespace Roblox.Application.Identity;

public interface ISessionTokenFactory
{
    SessionToken Create(DateTimeOffset now, TimeSpan lifetime);
    string Hash(string rawToken);
}
