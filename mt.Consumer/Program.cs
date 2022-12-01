using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using mt.Consumer;
using mt.Consumer.Consumers;
using mt.Contracts.Extensions;
using System.Reflection;

var builder = new HostBuilder()
    .ConfigureServices(services =>
    {
        services.AddOptions<NodeOptions>();

        services.AddMassTransit(cfg => 
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            cfg.AddConsumers(entryAssembly);

            cfg.UsingRabbitMq((context, cfg) => 
            {
                cfg.ConfigureDirectMessageTopology();
                cfg.ConfigureEndpoints(context);
            });
        });
    })
    .ConfigureLogging((hostingContext, logging) => {
        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        logging.AddConsole();
    });

await builder.RunConsoleAsync();