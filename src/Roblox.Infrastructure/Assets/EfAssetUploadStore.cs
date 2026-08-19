using Microsoft.EntityFrameworkCore;
using Roblox.Application.Assets;
using Roblox.Domain.Assets;
using Roblox.Infrastructure.Persistence;
namespace Roblox.Infrastructure.Assets;
public sealed class EfAssetUploadStore(RobloxDbContext db) : IAssetUploadStore
{
 public Task<AssetUpload?> FindAsync(Guid uploadId,long creatorUserId,CancellationToken ct)=>db.AssetUploads.SingleOrDefaultAsync(x=>x.Id==uploadId && x.CreatorUserId==creatorUserId,ct);
 public Task AddAsync(AssetUpload upload,CancellationToken ct)=>db.AssetUploads.AddAsync(upload,ct).AsTask();
 public Task SaveChangesAsync(CancellationToken ct)=>db.SaveChangesAsync(ct);
}
