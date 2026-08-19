using Roblox.Domain.Identity;

namespace Roblox.Application.Identity;

public sealed class AccountRegistrationService(
    IUserAccountStore userAccountStore,
    IUsernamePolicy usernamePolicy,
    IPasswordHasher passwordHasher,
    TimeProvider timeProvider)
{
    public async Task<RegistrationResult> RegisterAsync(
        string username,
        string password,
        DateOnly birthDate,
        UserGender gender,
        CancellationToken cancellationToken)
    {
        var usernameValidation = usernamePolicy.Validate(username);
        if (!usernameValidation.IsValid)
        {
            return new RegistrationResult(AccountResultCode.InvalidUsername, null);
        }

        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
        {
            return new RegistrationResult(AccountResultCode.InvalidPassword, null);
        }

        var normalizedUsername = UserAccount.NormalizeUsername(username);
        var existing = await userAccountStore.FindByNormalizedUsernameAsync(normalizedUsername, cancellationToken);
        if (existing is not null)
        {
            return new RegistrationResult(AccountResultCode.UsernameAlreadyInUse, null);
        }

        var now = timeProvider.GetUtcNow();
        var userAccount = new UserAccount(username, now);
        userAccount.SetProfile(birthDate, gender, now);
        var passwordHash = passwordHasher.Hash(password);
        userAccount.SetPasswordCredential(
            passwordHash.Algorithm,
            passwordHash.WorkFactor,
            passwordHash.Salt,
            passwordHash.Hash,
            now);

        var created = await userAccountStore.TryCreateAsync(userAccount, cancellationToken);
        return created
            ? new RegistrationResult(AccountResultCode.Success, userAccount.Id)
            : new RegistrationResult(AccountResultCode.UsernameAlreadyInUse, null);
    }
}
