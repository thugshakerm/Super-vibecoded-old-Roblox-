namespace Roblox.Application.Identity;

public interface IPasswordHasher
{
    PasswordHash Hash(string password);
    bool Verify(string password, PasswordHash passwordHash);
}
