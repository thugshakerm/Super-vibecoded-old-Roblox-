using Roblox.Domain.Assets;
namespace Roblox.Application.Assets;
public interface IAssetUploadStore { Task AddAsync(AssetUpload upload, CancellationToken cancellationToken); Task SaveChangesAsync(CancellationToken cancellationToken); }
