using Microsoft.Extensions.Options;
using Roblox.Website.Configuration;
using Roblox.Website.Security;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();
builder.Services.AddOptions<PlatformApiOptions>()
    .Bind(builder.Configuration.GetSection(PlatformApiOptions.SectionName))
    .Validate(options => Uri.TryCreate(options.BaseUrl, UriKind.Absolute, out _),
        "PlatformApi:BaseUrl must be an absolute URL.")
    .ValidateOnStart();
builder.Services.AddHttpClient("PlatformApi", (services, client) =>
{
    var options = services.GetRequiredService<IOptions<PlatformApiOptions>>().Value;
    client.BaseAddress = new Uri(options.BaseUrl, UriKind.Absolute);
});

var app = builder.Build();
app.UseExceptionHandler("/error");
app.UseWebsiteSecurityHeaders();
app.MapHealthChecks("/health");

// Historical Razor pages and the shared late-2013 shell are deliberately not
// scaffolded until their sanitized source capture is available.
app.Run();
