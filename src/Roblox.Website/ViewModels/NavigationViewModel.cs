namespace Roblox.Website.ViewModels;

/// <summary>Runtime values only; no archived account values are retained in the website component.</summary>
public sealed record NavigationViewModel(
    bool IsAuthenticated,
    string? Username,
    long Robux,
    long Tickets,
    int FriendRequestCount);
