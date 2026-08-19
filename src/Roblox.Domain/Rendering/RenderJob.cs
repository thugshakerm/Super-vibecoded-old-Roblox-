namespace Roblox.Domain.Rendering;

/// <summary>
/// Persistent work item for a trusted, internal RCC render. It never stores arbitrary user Lua.
/// </summary>
public sealed class RenderJob
{
    public Guid Id { get; private set; }
    public RenderJobKind Kind { get; private set; }
    public long TargetId { get; private set; }
    public long AppearanceOrAssetVersion { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public RenderJobState State { get; private set; }
    public int AttemptCount { get; private set; }
    public DateTimeOffset RequestedAt { get; private set; }
    public DateTimeOffset? LeaseExpiresAt { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }
    public string? FailureCode { get; private set; }
    public string? OutputObjectKey { get; private set; }

    private RenderJob() { }

    public RenderJob(
        RenderJobKind kind,
        long targetId,
        long appearanceOrAssetVersion,
        int width,
        int height,
        DateTimeOffset requestedAt)
    {
        if (targetId <= 0) throw new ArgumentOutOfRangeException(nameof(targetId));
        if (appearanceOrAssetVersion <= 0) throw new ArgumentOutOfRangeException(nameof(appearanceOrAssetVersion));
        if (width <= 0 || height <= 0 || width > 2048 || height > 2048)
            throw new ArgumentOutOfRangeException(nameof(width));

        Id = Guid.NewGuid();
        Kind = kind;
        TargetId = targetId;
        AppearanceOrAssetVersion = appearanceOrAssetVersion;
        Width = width;
        Height = height;
        State = RenderJobState.Queued;
        RequestedAt = requestedAt;
    }

    public void Lease(DateTimeOffset now, TimeSpan duration)
    {
        if (State != RenderJobState.Queued || duration <= TimeSpan.Zero)
            throw new InvalidOperationException("Only queued jobs can be leased.");

        State = RenderJobState.Leased;
        AttemptCount++;
        LeaseExpiresAt = now.Add(duration);
    }

    public void Complete(string outputObjectKey, DateTimeOffset now)
    {
        if (State != RenderJobState.Leased) throw new InvalidOperationException("Only leased jobs can complete.");
        if (string.IsNullOrWhiteSpace(outputObjectKey)) throw new ArgumentException("Output key is required.", nameof(outputObjectKey));

        State = RenderJobState.Completed;
        OutputObjectKey = outputObjectKey;
        CompletedAt = now;
        LeaseExpiresAt = null;
    }

    public void Fail(string failureCode)
    {
        if (State != RenderJobState.Leased) throw new InvalidOperationException("Only leased jobs can fail.");
        if (string.IsNullOrWhiteSpace(failureCode)) throw new ArgumentException("Failure code is required.", nameof(failureCode));

        State = RenderJobState.Failed;
        FailureCode = failureCode;
        LeaseExpiresAt = null;
    }
}
