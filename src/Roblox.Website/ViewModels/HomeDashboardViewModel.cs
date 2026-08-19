namespace Roblox.Website.ViewModels;

public sealed record HomeDashboardViewModel(
    string Username,
    string? AvatarThumbnailUrl,
    int SystemNotificationCount,
    IReadOnlyList<HomeNewsItem> News,
    IReadOnlyList<HomeFriendItem> BestFriends,
    IReadOnlyList<HomeRecentPlaceItem> RecentlyPlayed);

public sealed record HomeNewsItem(string Title, string Url);
public sealed record HomeFriendItem(string Username, string ProfileUrl, string? AvatarThumbnailUrl, string? PresenceIconUrl, string? Status);
public sealed record HomeRecentPlaceItem(string Name, string PlaceUrl, string? ThumbnailUrl, int PlayerCount);
