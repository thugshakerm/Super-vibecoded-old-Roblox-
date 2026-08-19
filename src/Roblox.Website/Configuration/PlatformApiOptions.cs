namespace Roblox.Website.Configuration;

/// <summary>Server-side endpoint for the independently runnable platform API.</summary>
internal sealed class PlatformApiOptions
{
    public const string SectionName = "PlatformApi";
    public string BaseUrl { get; init; } = string.Empty;
}
