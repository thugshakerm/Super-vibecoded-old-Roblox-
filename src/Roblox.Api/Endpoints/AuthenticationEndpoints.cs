using Roblox.Api.Authentication;
using Roblox.Application.Identity;

namespace Roblox.Api.Endpoints;

internal static class AuthenticationEndpoints
{
    public static RouteGroupBuilder MapAuthenticationEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/auth")
            .RequireRateLimiting("authentication");

        group.MapPost("/register", RegisterAsync);
        group.MapPost("/login", SignInAsync);
        group.MapPost("/logout", SignOutAsync).RequirePlatformSession();
        group.MapGet("/session", GetSessionAsync).RequirePlatformSession();
        return group;
    }

    private static async Task<IResult> RegisterAsync(
        RegistrationRequest request,
        AccountRegistrationService registrations,
        CancellationToken cancellationToken)
    {
        if (request.BirthDate is null || request.Gender is null) return Results.BadRequest(new { code = "InvalidProfile" });
        var result = await registrations.RegisterAsync(request.Username, request.Password, request.BirthDate.Value, request.Gender.Value, cancellationToken);
        return result.Code switch
        {
            AccountResultCode.Success => Results.Created($"/api/users/{result.UserId}", new { userId = result.UserId }),
            AccountResultCode.UsernameAlreadyInUse => Results.Conflict(new { code = "UsernameAlreadyInUse" }),
            AccountResultCode.InvalidUsername => Results.BadRequest(new { code = "InvalidUsername" }),
            AccountResultCode.InvalidPassword => Results.BadRequest(new { code = "InvalidPassword" }),
            _ => Results.Problem(statusCode: StatusCodes.Status400BadRequest, title: "Registration failed.")
        };
    }

    private static async Task<IResult> SignInAsync(
        SignInRequest request,
        AccountSignInService signIns,
        IWebHostEnvironment environment,
        HttpResponse response,
        CancellationToken cancellationToken)
    {
        var result = await signIns.SignInAsync(request.Username, request.Password, cancellationToken);
        if (!result.Succeeded || result.Session is null)
        {
            // Do not disclose whether the username exists or why a non-active account cannot sign in.
            return Results.Unauthorized();
        }

        response.Cookies.Append(PlatformSessionCookie.Name, result.Session.Value, new CookieOptions
        {
            HttpOnly = true,
            Secure = !environment.IsDevelopment(),
            SameSite = SameSiteMode.Lax,
            Expires = result.Session.ExpiresAt,
            IsEssential = true,
            Path = "/"
        });

        return Results.NoContent();
    }

    private static async Task<IResult> SignOutAsync(
        HttpRequest request,
        HttpResponse response,
        ISessionAuthenticationService sessions,
        CancellationToken cancellationToken)
    {
        if (request.Cookies.TryGetValue(PlatformSessionCookie.Name, out var rawToken))
        {
            await sessions.RevokeAsync(rawToken, cancellationToken);
        }

        response.Cookies.Delete(PlatformSessionCookie.Name, new CookieOptions { Path = "/" });
        return Results.NoContent();
    }

    private static IResult GetSessionAsync(HttpContext context)
    {
        var session = (AuthenticatedSession)context.Items[typeof(AuthenticatedSession)]!;
        return Results.Ok(new { userId = session.UserId, expiresAt = session.ExpiresAt });
    }

    internal sealed record RegistrationRequest(string Username, string Password, DateOnly? BirthDate, Roblox.Domain.Identity.UserGender? Gender);
    internal sealed record SignInRequest(string Username, string Password);
}
