using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Roblox.Website.Pages.Login;

public sealed class DefaultModel(IHttpClientFactory clients) : PageModel
{
    [BindProperty] public LoginInput Input { get; set; } = new();
    public string? ErrorMessage { get; private set; }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        var response = await clients.CreateClient("PlatformApi").PostAsJsonAsync("/api/auth/login", Input, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            foreach (var cookie in response.Headers.GetValues("Set-Cookie")) Response.Headers.Append("Set-Cookie", cookie);
            return LocalRedirect("/home");
        }
        ErrorMessage = "Login failed.";
        return Page();
    }

    public sealed record LoginInput(string Username = "", string Password = "");
}
