using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using mt.Contracts.ExchangeDirectContracts;
using RabbitMQ.Client;

namespace mt.Consumer.Consumers;

public class ExchangeDirectConsumer : IConsumer<ExchangeDirectContract>
{
    private readonly ILogger _logger;

    public ExchangeDirectConsumer(ILogger<ExchangeDirectConsumer> logger)
    {
        _logger= logger;
    }

    public Task Consume(ConsumeContext<ExchangeDirectContract> context)
    {
        _logger.LogInformation($"{context.Message.Id} {context.Message.NodeId} {context.Message.Name} {context.Message.Amount}");

        return Task.CompletedTask;
    }
}

public class ExchangeDirectorConsumerDefinition : ConsumerDefinition<ExchangeDirectConsumer>
{
    private readonly string _nodeId;

    public ExchangeDirectorConsumerDefinition(IOptions<NodeOptions> options)
    {
        _nodeId = options.Value.NodeId;
        EndpointName = $"direct.client.500";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<ExchangeDirectConsumer> consumerConfigurator)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.Bind<ExchangeDirectContract>(x =>
            {
                x.RoutingKey= "500";
                x.ExchangeType = ExchangeType.Direct;
            });
        }
    }
}