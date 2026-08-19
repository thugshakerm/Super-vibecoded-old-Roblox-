namespace Roblox.Domain.Assets;

/// <summary>Private upload intake record. It is not publicly deliverable until approved.</summary>
public sealed class AssetUpload
{
    public Guid Id { get; private set; }
    public long CreatorUserId { get; private set; }
    public AssetType AssetType { get; private set; }
    public AssetUploadState State { get; private set; }
    public string OriginalFileName { get; private set; } = string.Empty;
    public string ExpectedContentType { get; private set; } = string.Empty;
    public long MaximumContentLength { get; private set; }
    public string PrivateObjectKey { get; private set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset ExpiresAt { get; private set; }
    public DateTimeOffset? UploadedAt { get; private set; }
    public string? FailureCode { get; private set; }

    private AssetUpload() { }
    public void MarkUploaded(DateTimeOffset now)
    {
        if (State != AssetUploadState.Created || now > ExpiresAt) throw new InvalidOperationException("Upload is unavailable.");
        State = AssetUploadState.Uploaded; UploadedAt = now;
    }

    public AssetUpload(long creatorUserId, AssetType assetType, string fileName, string contentType, long maxLength, string objectKey, DateTimeOffset now, DateTimeOffset expiresAt)
    {
        if (creatorUserId <= 0 || maxLength <= 0 || expiresAt <= now) throw new ArgumentOutOfRangeException(nameof(creatorUserId));
        if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(contentType) || string.IsNullOrWhiteSpace(objectKey)) throw new ArgumentException("Upload metadata is required.");
        Id = Guid.NewGuid(); CreatorUserId = creatorUserId; AssetType = assetType; State = AssetUploadState.Created;
        OriginalFileName = fileName; ExpectedContentType = contentType; MaximumContentLength = maxLength; PrivateObjectKey = objectKey;
        CreatedAt = now; ExpiresAt = expiresAt;
    }
}
