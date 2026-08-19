using Roblox.Application.Assets;

namespace Roblox.Api.Endpoints;

internal static class AssetDeliveryEndpoints
{
    public static RouteGroupBuilder MapAssetDeliveryEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/assets")
            .RequireRateLimiting("public-api");

        group.MapGet("/{assetId:long}/thumbnails/{width:int}x{height:int}", GetThumbnail)
            .WithName("GetAssetThumbnailDelivery")
            .Produces<AssetThumbnailDeliveryResponse>();

        return group;
    }

    private static async Task<IResult> GetThumbnail(
        long assetId,
        int width,
        int height,
        IAssetDeliveryService assetDeliveryService,
        CancellationToken cancellationToken)
    {
        var result = await assetDeliveryService.GetThumbnailDeliveryAsync(
            assetId, width, height, cancellationToken);
        return Results.Ok(AssetThumbnailDeliveryResponse.From(result));
    }
}
