namespace Roblox.Application.Rendering;

public interface IRccRenderClient
{
    Task<RccRenderResult> RenderAsync(RccRenderRequest request, CancellationToken cancellationToken);
}
