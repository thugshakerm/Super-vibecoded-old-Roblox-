using Roblox.Domain.Assets;
namespace Roblox.Application.Assets;
public interface IAssetUploadPolicy
{
    bool TryGetMaximumLength(AssetType assetType, string contentType, out long maximumLength);
}
