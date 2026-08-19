namespace Roblox.Domain.Assets;

public enum AssetUploadState
{
    Created = 0,
    Uploading = 1,
    Uploaded = 2,
    Validating = 3,
    PendingModeration = 4,
    Approved = 5,
    Rejected = 6,
    Quarantined = 7
}
