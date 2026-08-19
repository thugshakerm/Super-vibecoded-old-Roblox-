using Roblox.Domain.Identity;

namespace Roblox.Application.Identity;

public sealed class AccountSignInService(
    IUserAccountStore userAccountStore,
    ISessionStore sessionStore,
    IPasswordHasher passwordHasher,
    ISessionTokenFactory sessionTokenFactory,
    TimeProvider timeProvider)
{
    private static readonly TimeSpan SessionLifetime = TimeSpan.FromHours(12);

    public async Task<SignInResult> SignInAsync(
        string username,
        string password,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return new SignInResult(AccountResultCode.InvalidCredentials, null, null);
        }

        var userAccount = await userAccountStore.FindByNormalizedUsernameAsync(
            UserAccount.NormalizeUsername(username), cancellationToken);

        if (userAccount?.PasswordCredential is null)
        {
            return new SignInResult(AccountResultCode.InvalidCredentials, null, null);
        }

        var credential = userAccount.PasswordCredential;
        var validPassword = passwordHasher.Verify(password, new PasswordHash(
            credential.Algorithm,
            credential.WorkFactor,
            credential.Salt,
            credential.Hash));

        if (!validPassword)
        {
            return new SignInResult(AccountResultCode.InvalidCredentials, null, null);
        }

        if (userAccount.State != UserAccountState.Active)
        {
            return new SignInResult(AccountResultCode.AccountUnavailable, userAccount.Id, null);
        }

        var now = timeProvider.GetUtcNow();
        var token = sessionTokenFactory.Create(now, SessionLifetime);
        var session = new UserSession(userAccount.Id, token.Hash, now, token.ExpiresAt);

        await sessionStore.AddAsync(session, cancellationToken);
        await sessionStore.SaveChangesAsync(cancellationToken);
        return new SignInResult(AccountResultCode.Success, userAccount.Id, token);
    }
}
