using Roblox.Application.Identity;

namespace Roblox.Api.Authentication;

internal static class PlatformSessionExtensions
{
    public static RouteHandlerBuilder RequirePlatformSession(this RouteHandlerBuilder endpoint) =>
        endpoint.AddEndpointFilter(async (context, next) =>
        {
            var httpContext = context.HttpContext;
            if (!httpContext.Request.Cookies.TryGetValue(PlatformSessionCookie.Name, out var rawToken))
            {
                return Results.Unauthorized();
            }

            var sessions = httpContext.RequestServices.GetRequiredService<ISessionAuthenticationService>();
            var session = await sessions.ValidateAsync(rawToken, httpContext.RequestAborted);
            if (session is null)
            {
                return Results.Unauthorized();
            }

            httpContext.Items[typeof(AuthenticatedSession)] = session;
            return await next(context);
        });
}
