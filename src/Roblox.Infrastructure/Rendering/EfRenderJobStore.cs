using Roblox.Application.Rendering;
using Roblox.Domain.Rendering;
using Roblox.Infrastructure.Persistence;

namespace Roblox.Infrastructure.Rendering;

public sealed class EfRenderJobStore(RobloxDbContext dbContext) : IRenderJobStore
{
    public Task AddAsync(RenderJob job, CancellationToken cancellationToken) =>
        dbContext.RenderJobs.AddAsync(job, cancellationToken).AsTask();

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        dbContext.SaveChangesAsync(cancellationToken);
}
