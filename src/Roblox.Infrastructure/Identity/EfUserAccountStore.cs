using Microsoft.EntityFrameworkCore;
using Npgsql;
using Roblox.Application.Identity;
using Roblox.Domain.Identity;
using Roblox.Infrastructure.Persistence;

namespace Roblox.Infrastructure.Identity;

public sealed class EfUserAccountStore(RobloxDbContext dbContext) : IUserAccountStore
{
    public Task<UserAccount?> FindByIdAsync(long userId, CancellationToken cancellationToken) =>
        dbContext.UserAccounts.Include(user => user.Profile).SingleOrDefaultAsync(user => user.Id == userId, cancellationToken);

    public Task<UserAccount?> FindByNormalizedUsernameAsync(string normalizedUsername, CancellationToken cancellationToken) =>
        dbContext.UserAccounts
            .Include(user => user.PasswordCredential)
            .SingleOrDefaultAsync(user => user.NormalizedUsername == normalizedUsername, cancellationToken);

    public async Task<bool> TryCreateAsync(UserAccount userAccount, CancellationToken cancellationToken)
    {
        await dbContext.UserAccounts.AddAsync(userAccount, cancellationToken);
        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (DbUpdateException exception) when (
            exception.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation })
        {
            return false;
        }
    }
}
