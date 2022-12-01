using MassTransit;
using mt.Contracts.ExchangeDirectContracts;
using RabbitMQ.Client;

namespace mt.Contracts.Extensions;

public static class ExchangeDirectConfigurationExtensions
{
    public static void ConfigureDirectMessageTopology(this IRabbitMqBusFactoryConfigurator configurator)
    {
        //configurator.Message<ExchangeDirectContract>(x => x.SetEntityName("content.received"));

        configurator.Send<ExchangeDirectContract>(x =>
        {
            x.UseCorrelationId(context => context.Id);
            x.UseRoutingKeyFormatter(context => context.Message.NodeId);
        });

        configurator.Publish<ExchangeDirectContract>(x =>
        {
            x.ExchangeType = ExchangeType.Direct;
        });
    }
}
