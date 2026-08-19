namespace Roblox.Domain.Economy;

/// <summary>Immutable currency ledger entry; balances are derived/maintained from this audit trail.</summary>
public sealed class CurrencyTransaction
{
    public Guid Id { get; private set; }
    public long UserAccountId { get; private set; }
    public CurrencyType Currency { get; private set; }
    public long Amount { get; private set; }
    public string TransactionType { get; private set; } = string.Empty;
    public Guid? CorrelationId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    private CurrencyTransaction() { }
}
