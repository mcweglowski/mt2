using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = new HostBuilder()
    .ConfigureServices(services =>
    {
    })
    .ConfigureLogging((hostingContext, logging) => {
        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        logging.AddConsole();
    });

builder.RunConsoleAsync();