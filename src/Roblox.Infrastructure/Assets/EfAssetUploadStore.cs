using Roblox.Application.Assets;
using Roblox.Domain.Assets;
using Roblox.Infrastructure.Persistence;
namespace Roblox.Infrastructure.Assets;
public sealed class EfAssetUploadStore(RobloxDbContext db) : IAssetUploadStore
{
 public Task AddAsync(AssetUpload upload,CancellationToken ct)=>db.AssetUploads.AddAsync(upload,ct).AsTask();
 public Task SaveChangesAsync(CancellationToken ct)=>db.SaveChangesAsync(ct);
}
