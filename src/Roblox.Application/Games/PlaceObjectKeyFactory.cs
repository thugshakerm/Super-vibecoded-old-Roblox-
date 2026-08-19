using Roblox.Domain.Assets;

namespace Roblox.Application.Games;

public static class PlaceObjectKeyFactory
{
    public static ObjectKey Version(long placeId, int version) =>
        ObjectKey.Create($"places/{placeId}/versions/{version}/place");

    public static ObjectKey Thumbnail(long placeId, int version, int width, int height) =>
        ObjectKey.Create($"thumbnails/places/{placeId}/versions/{version}/{width}x{height}.png");
}
