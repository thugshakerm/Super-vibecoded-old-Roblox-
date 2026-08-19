namespace Roblox.Application.Identity;

public interface ISessionAuthenticationService
{
    Task<AuthenticatedSession?> ValidateAsync(string rawToken, CancellationToken cancellationToken);
    Task RevokeAsync(string rawToken, CancellationToken cancellationToken);
}
