namespace Roblox.Application.Economy;

/// <summary>
/// RAP policy is intentionally isolated. Its exact historical formula must be approved from
/// period/reference evidence before it is enabled for production market values.
/// </summary>
public interface IRapCalculator
{
    long? Calculate(IReadOnlyList<long> completedSalePrices);
}
