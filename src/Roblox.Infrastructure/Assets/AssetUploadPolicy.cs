using Roblox.Application.Assets;
using Roblox.Domain.Assets;
namespace Roblox.Infrastructure.Assets;
/// <summary>Initial conservative allow-list. Validation workers perform deeper format/content checks later.</summary>
public sealed class AssetUploadPolicy : IAssetUploadPolicy
{
 public bool TryGetMaximumLength(AssetType type,string contentType,out long max)
 {
  max=0;
  if (type is AssetType.Image or AssetType.Decal or AssetType.Shirt or AssetType.Pants && contentType is "image/png" or "image/jpeg") { max=10*1024*1024; return true; }
  if (type is AssetType.Model && contentType is "application/octet-stream" or "application/xml") { max=50*1024*1024; return true; }
  if (type is AssetType.Audio && contentType.StartsWith("audio/",StringComparison.OrdinalIgnoreCase)) { max=20*1024*1024; return true; }
  return false;
 }
}
