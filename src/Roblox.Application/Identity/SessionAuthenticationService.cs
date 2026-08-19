namespace Roblox.Application.Identity;

public sealed class SessionAuthenticationService(
    ISessionStore sessionStore,
    ISessionTokenFactory sessionTokenFactory,
    TimeProvider timeProvider) : ISessionAuthenticationService
{
    public Task<AuthenticatedSession?> ValidateAsync(string rawToken, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(rawToken)) return Task.FromResult<AuthenticatedSession?>(null);

        try
        {
            return sessionStore.FindActiveAsync(
                sessionTokenFactory.Hash(rawToken), timeProvider.GetUtcNow(), cancellationToken);
        }
        catch (FormatException)
        {
            return Task.FromResult<AuthenticatedSession?>(null);
        }
    }

    public Task RevokeAsync(string rawToken, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(rawToken)) return Task.CompletedTask;

        try
        {
            return sessionStore.RevokeAsync(
                sessionTokenFactory.Hash(rawToken), timeProvider.GetUtcNow(), cancellationToken);
        }
        catch (FormatException)
        {
            // A malformed client cookie is not a server error and has nothing to revoke.
            return Task.CompletedTask;
        }
    }
}
