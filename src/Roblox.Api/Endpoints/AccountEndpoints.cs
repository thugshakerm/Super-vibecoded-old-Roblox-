using Roblox.Api.Authentication;
using Roblox.Application.Identity;
namespace Roblox.Api.Endpoints;
internal static class AccountEndpoints
{
 public static RouteGroupBuilder MapAccountEndpoints(this IEndpointRouteBuilder endpoints)
 {
  var group=endpoints.MapGroup("/api/account").RequireRateLimiting("public-api");
  group.MapGet("/profile", GetProfileAsync).RequirePlatformSession(); return group;
 }
 private static async Task<IResult> GetProfileAsync(HttpContext context,IAccountProfileService profiles,CancellationToken ct)
 {
  var session=(AuthenticatedSession)context.Items[typeof(AuthenticatedSession)]!;
  var profile=await profiles.GetAsync(session.UserId,ct);
  return profile is null ? Results.NotFound() : Results.Ok(profile);
 }
}
