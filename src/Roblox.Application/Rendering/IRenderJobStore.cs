using Roblox.Domain.Rendering;

namespace Roblox.Application.Rendering;

public interface IRenderJobStore
{
    Task AddAsync(RenderJob job, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
