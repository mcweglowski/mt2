using MassTransit;
using Microsoft.Extensions.Logging;
using mt.Contracts.RemoteProcedureCallContracts;

public class RemoteProcedureCallConsumer : IConsumer<IRemoteProcedureCallContract>
{
    private readonly ILogger _logger;
    public RemoteProcedureCallConsumer(ILogger<RemoteProcedureCallConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<IRemoteProcedureCallContract> context)
    {
        _logger.LogInformation($"{context.Message.Id} {context.Message.Name} {context.Message.Amount}");

        await context.RespondAsync<IRemoteProcedureCallResponse>(new 
        {
            Id = context.Message.Id,
            Name = context.Message.Name,
            Amount = context.Message.Amount
        });
    }
}
