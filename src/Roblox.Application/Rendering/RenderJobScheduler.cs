using Roblox.Domain.Rendering;

namespace Roblox.Application.Rendering;

public sealed class RenderJobScheduler(IRenderJobStore renderJobStore, TimeProvider timeProvider)
{
    public async Task<Guid> ScheduleAsync(
        RenderJobKind kind,
        long targetId,
        long appearanceOrAssetVersion,
        int width,
        int height,
        CancellationToken cancellationToken)
    {
        var job = new RenderJob(kind, targetId, appearanceOrAssetVersion, width, height, timeProvider.GetUtcNow());
        await renderJobStore.AddAsync(job, cancellationToken);
        await renderJobStore.SaveChangesAsync(cancellationToken);
        return job.Id;
    }
}
