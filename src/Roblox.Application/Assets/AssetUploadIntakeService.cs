using Roblox.Domain.Assets;
namespace Roblox.Application.Assets;
public sealed class AssetUploadIntakeService(IAssetUploadPolicy policy, IAssetUploadStore store, TimeProvider timeProvider)
{
    public async Task<AssetUpload?> CreateAsync(long userId, AssetType type, string fileName, string contentType, CancellationToken ct)
    {
        if (!policy.TryGetMaximumLength(type, contentType, out var max)) return null;
        var now=timeProvider.GetUtcNow();
        var key=ObjectKey.Create($"quarantine/uploads/{Guid.NewGuid():N}").Value;
        var upload=new AssetUpload(userId,type,fileName,contentType,max,key,now,now.AddMinutes(15));
        await store.AddAsync(upload,ct); await store.SaveChangesAsync(ct); return upload;
    }
}
