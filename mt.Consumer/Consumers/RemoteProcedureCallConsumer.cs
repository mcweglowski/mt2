using MassTransit;
using Microsoft.Extensions.Logging;
using mt.Contracts.Contracts;

namespace mt.Consumer.Consumers;

public class RemoteProcedureCallConsumer : IConsumer<IRemoteProcedureCallContract>
{
    private readonly ILogger _logger;
    public RemoteProcedureCallConsumer(ILogger<RemoteProcedureCallConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<IRemoteProcedureCallContract> context)
    {
        _logger.LogInformation($"{context.Message.Id} {context.Message.Name} {context.Message.Amount}");

        return Task.CompletedTask;
    }
}
