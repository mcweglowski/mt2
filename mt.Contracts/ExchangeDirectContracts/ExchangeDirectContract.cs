namespace mt.Contracts.ExchangeDirectContracts;

public interface ExchangeDirectContract
{
    Guid Id { get; }
    string NodeId { get; }
    public string Name { get; }
    public decimal Amount { get; }
}
