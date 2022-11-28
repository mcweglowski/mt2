using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using mt.Consumer.Consumers;

var builder = new HostBuilder()
    .ConfigureServices(services =>
    {
        services.AddMassTransit(cfg => 
        {
            cfg.AddConsumer<RemoteProcedureCallConsumer>();

            cfg.UsingRabbitMq((context, cfg) => 
            {
                cfg.ConfigureEndpoints(context);
            });
        });
    })
    .ConfigureLogging((hostingContext, logging) => {
        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        logging.AddConsole();
    });

await builder.RunConsoleAsync();