using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.RateLimiting;
using Roblox.Api.Configuration;
using Roblox.Api.Endpoints;
using Roblox.Api.Security;
using Roblox.Infrastructure;
using Roblox.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

var securityOptions = builder.Configuration
    .GetSection(ApiSecurityOptions.SectionName)
    .Get<ApiSecurityOptions>() ?? new ApiSecurityOptions();

if (securityOptions.FrontendOrigins.Length == 0)
{
    throw new InvalidOperationException("At least one explicit ApiSecurity:FrontendOrigins value is required.");
}

builder.Services.AddProblemDetails();
builder.Services.AddRobloxInfrastructure(builder.Configuration);
builder.Services.AddHealthChecks().AddDbContextCheck<RobloxDbContext>("postgres");
builder.Services.AddCors(options => options.AddPolicy("website", policy => policy
    .WithOrigins(securityOptions.FrontendOrigins)
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()));
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.Headers["Retry-After"] = "60";
        await Results.Problem(
            statusCode: StatusCodes.Status429TooManyRequests,
            title: "Too many requests",
            detail: "Try again in one minute.")
            .ExecuteAsync(context.HttpContext);
    };
    options.AddPolicy("authentication", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: $"{httpContext.Connection.RemoteIpAddress}:{httpContext.Request.Path}",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromMinutes(10),
                QueueLimit = 0,
                AutoReplenishment = true
            }));
    options.AddPolicy("public-api", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = securityOptions.RequestsPerMinute,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0,
                AutoReplenishment = true
            }));
});

var app = builder.Build();

app.UseExceptionHandler(exceptionApp => exceptionApp.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
    await Results.Problem(
        statusCode: StatusCodes.Status500InternalServerError,
        title: "An unexpected platform error occurred.",
        detail: app.Environment.IsDevelopment() ? exception?.Message : null)
        .ExecuteAsync(context);
}));
app.UsePlatformSecurityHeaders();
app.UseCors("website");
app.UseRateLimiter();

app.MapHealthChecks("/health");
app.MapGet("/api/status", () => Results.Ok(new { service = "Roblox.Api", status = "security-foundation" }))
    .RequireRateLimiting("public-api");
app.MapAssetDeliveryEndpoints();
app.MapAuthenticationEndpoints();
app.MapAssetUploadEndpoints();
app.MapAccountEndpoints();

app.Run();
