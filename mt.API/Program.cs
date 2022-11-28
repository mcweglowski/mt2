using MassTransit;
using mt.Contracts.RemoteProcedureCallContracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMassTransit(cfg => 
{
    cfg.UsingRabbitMq((context, config) =>
    {
        config.ConfigureEndpoints(context);
    });

    cfg.AddRequestClient<IRemoteProcedureCallContract>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
