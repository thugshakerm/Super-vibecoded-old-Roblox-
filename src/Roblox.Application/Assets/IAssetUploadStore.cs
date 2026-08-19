using Roblox.Domain.Assets;
namespace Roblox.Application.Assets;
public interface IAssetUploadStore { Task<AssetUpload?> FindAsync(Guid uploadId, long creatorUserId, CancellationToken cancellationToken); Task AddAsync(AssetUpload upload, CancellationToken cancellationToken); Task SaveChangesAsync(CancellationToken cancellationToken); }
