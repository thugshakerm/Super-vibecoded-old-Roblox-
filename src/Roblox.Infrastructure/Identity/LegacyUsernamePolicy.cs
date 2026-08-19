using System.Text.RegularExpressions;
using Roblox.Application.Identity;

namespace Roblox.Infrastructure.Identity;

/// <summary>
/// Storage-safe username policy for the 2012–2014 target range. Final public copy and UI validation
/// remain tied to the selected historical account-creation source.
/// </summary>
public sealed partial class LegacyUsernamePolicy : IUsernamePolicy
{
    public UsernameValidationResult Validate(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            return new UsernameValidationResult(false, "Username is required.");
        }

        var trimmed = username.Trim();
        if (!UsernamePattern().IsMatch(trimmed))
        {
            return new UsernameValidationResult(false, "Username must be 3–20 letters, numbers, or underscores.");
        }

        return UsernameValidationResult.Valid;
    }

    [GeneratedRegex("^[A-Za-z0-9_]{3,20}$", RegexOptions.CultureInvariant)]
    private static partial Regex UsernamePattern();
}
