namespace Roblox.Domain.Assets;

/// <summary>Initial period-relevant asset categories. New categories require an explicit migration.</summary>
public enum AssetType
{
    Unknown = 0,
    Image = 1,
    Decal = 2,
    Model = 3,
    Place = 4,
    Hat = 5,
    Gear = 6,
    Shirt = 7,
    Pants = 8,
    Badge = 9,
    Audio = 10
}
