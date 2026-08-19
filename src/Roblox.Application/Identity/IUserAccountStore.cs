using Roblox.Domain.Identity;

namespace Roblox.Application.Identity;

public interface IUserAccountStore
{
    Task<UserAccount?> FindByIdAsync(long userId, CancellationToken cancellationToken);
    Task<UserAccount?> FindByNormalizedUsernameAsync(string normalizedUsername, CancellationToken cancellationToken);
    /// <summary>Persists once and returns false for a normalized-username uniqueness conflict.</summary>
    Task<bool> TryCreateAsync(UserAccount userAccount, CancellationToken cancellationToken);
}
