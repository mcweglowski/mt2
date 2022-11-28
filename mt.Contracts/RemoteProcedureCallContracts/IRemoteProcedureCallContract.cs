namespace mt.Contracts.RemoteProcedureCallContracts;

public interface IRemoteProcedureCallContract
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
}
