namespace Roblox.Application.Identity;

public interface IUsernamePolicy
{
    UsernameValidationResult Validate(string username);
}
