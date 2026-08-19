using Roblox.Api.Authentication;
using Roblox.Application.Assets;
using Roblox.Application.Identity;
using Roblox.Domain.Assets;

namespace Roblox.Api.Endpoints;

internal static class AssetUploadEndpoints
{
    public static RouteGroupBuilder MapAssetUploadEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/assets/uploads")
            .RequireRateLimiting("public-api");
        group.MapPost("/intake", CreateIntakeAsync).RequirePlatformSession();
        return group;
    }

    private static async Task<IResult> CreateIntakeAsync(
        AssetUploadIntakeRequest request,
        HttpContext context,
        AssetUploadIntakeService uploads,
        CancellationToken cancellationToken)
    {
        var session = (AuthenticatedSession)context.Items[typeof(AuthenticatedSession)]!;
        var upload = await uploads.CreateAsync(
            session.UserId,
            request.AssetType,
            request.FileName,
            request.ContentType,
            cancellationToken);

        if (upload is null)
        {
            return Results.BadRequest(new { code = "UnsupportedUploadType" });
        }

        // Object keys are intentionally omitted. A later authenticated upload endpoint resolves
        // the internal key by upload ID after checking ownership, expiry, and upload state.
        return Results.Created($"/api/assets/uploads/{upload.Id}", new
        {
            uploadId = upload.Id,
            expiresAt = upload.ExpiresAt,
            maximumContentLength = upload.MaximumContentLength
        });
    }

    internal sealed record AssetUploadIntakeRequest(AssetType AssetType, string FileName, string ContentType);
}
