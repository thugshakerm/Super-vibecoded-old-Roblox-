using Roblox.Domain.Identity;
namespace Roblox.Application.Identity;
public sealed class AccountProfileService(IUserAccountStore store) : IAccountProfileService
{
 public async Task<AccountProfile?> GetAsync(long userId, CancellationToken ct)
 {
  var user=await store.FindByIdAsync(userId,ct);
  return user?.Profile is { } p ? new AccountProfile(user.Id,user.Username,p.BirthDate,p.Gender.ToString()) : null;
 }
}
