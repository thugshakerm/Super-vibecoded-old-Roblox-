namespace Roblox.Application.Identity;
public interface IAccountProfileService { Task<AccountProfile?> GetAsync(long userId, CancellationToken cancellationToken); }
