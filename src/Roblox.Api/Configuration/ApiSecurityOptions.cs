namespace Roblox.Api.Configuration;

/// <summary>
/// Browser origins allowed to call the separately hosted API. Wildcards are deliberately unsupported.
/// </summary>
internal sealed class ApiSecurityOptions
{
    public const string SectionName = "ApiSecurity";
    public string[] FrontendOrigins { get; init; } = [];
    public int RequestsPerMinute { get; init; } = 120;
}
