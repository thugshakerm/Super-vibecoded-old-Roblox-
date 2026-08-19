using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Roblox.Website.Pages.Login;

public sealed class SignupModel(IHttpClientFactory clients) : PageModel
{
    [BindProperty] public SignupInput Input { get; set; } = new();
    public string? ErrorMessage { get; private set; }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!string.Equals(Input.Password, Input.ConfirmPassword, StringComparison.Ordinal)) { ErrorMessage = "Passwords do not match."; return Page(); }
        var response = await clients.CreateClient("PlatformApi").PostAsJsonAsync("/api/auth/register", new { Input.Username, Input.Password }, cancellationToken);
        if (response.StatusCode == HttpStatusCode.Created) return LocalRedirect("/Login/Default.aspx");
        ErrorMessage = response.StatusCode == HttpStatusCode.Conflict ? "That username is already in use." : "Sign up failed.";
        return Page();
    }

    public sealed record SignupInput(string Username = "", string Password = "", string ConfirmPassword = "", int? BirthMonth = null, int? BirthDay = null, int? BirthYear = null, string? Gender = null);
}
