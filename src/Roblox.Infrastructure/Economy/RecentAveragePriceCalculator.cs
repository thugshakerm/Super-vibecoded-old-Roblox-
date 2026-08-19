using Roblox.Application.Economy;

namespace Roblox.Infrastructure.Economy;

/// <summary>
/// Conservative arithmetic-average implementation over caller-selected completed sales.
/// The selection window/count is a policy decision and is not hardcoded as historical fact.
/// </summary>
public sealed class RecentAveragePriceCalculator : IRapCalculator
{
    public long? Calculate(IReadOnlyList<long> completedSalePrices)
    {
        if (completedSalePrices.Count == 0) return null;
        if (completedSalePrices.Any(price => price <= 0)) throw new ArgumentOutOfRangeException(nameof(completedSalePrices));
        return checked(completedSalePrices.Sum() / completedSalePrices.Count);
    }
}
