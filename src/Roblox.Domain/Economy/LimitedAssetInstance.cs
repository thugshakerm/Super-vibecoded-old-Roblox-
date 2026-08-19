namespace Roblox.Domain.Economy;

/// <summary>One individually owned serial-numbered copy of a limited asset.</summary>
public sealed class LimitedAssetInstance
{
    public long Id { get; private set; }
    public long AssetId { get; private set; }
    public long OwnerUserId { get; private set; }
    public int SerialNumber { get; private set; }
    public bool IsForSale { get; private set; }
    public long? AskingPrice { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    private LimitedAssetInstance() { }
}
