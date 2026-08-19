namespace Roblox.Domain.Economy;

/// <summary>Immutable completed limited resale event used for sale history and RAP calculation.</summary>
public sealed class LimitedSale
{
    public long Id { get; private set; }
    public long AssetId { get; private set; }
    public long LimitedAssetInstanceId { get; private set; }
    public long SellerUserId { get; private set; }
    public long BuyerUserId { get; private set; }
    public long SalePrice { get; private set; }
    public DateTimeOffset SoldAt { get; private set; }

    private LimitedSale() { }
}
