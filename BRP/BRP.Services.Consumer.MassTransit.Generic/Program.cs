using BRP.Services.Consumer.MassTransit.Generic.Consumer;
using BRP.Services.Consumer.MassTransit.Generic.Extensions;
using MassTransit;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

// load and check configs
var seqUri = builder.Configuration["seq_uri"];
if (string.IsNullOrEmpty(seqUri))
    throw new ArgumentException($"Falha ao iniciar {assemblyName}. Parâmetro seq_uri não encontrado.");

var rabbitMqUri = builder.Configuration["rabbitmq_uri"];
if (string.IsNullOrEmpty(rabbitMqUri))
    throw new ArgumentException($"Falha ao iniciar {assemblyName}. Parâmetro rabbitmq_uri não encontrado.");

// Add Log SEQ and HealthChecks
builder.ConfigureLogs(seqUri);
builder.ConfigureHealthChecks();

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddResponseCaching();
builder.Services.AddHttpContextAccessor();

// Add services MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ServiceConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqUri);
        cfg.ReceiveEndpoint("rows.masstransit", ep =>
        {
            ep.PrefetchCount = 10;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<ServiceConsumer>(context);
            cfg.ConfigureEndpoints(context);
        });
    });
});
builder.Services.AddMassTransitHostedService(true);

var app = builder.Build();

app.UseExceptionHandler("/error");

app.UseAuthorization();

Log.Information("Iniciando serviço {AssemblyName}", assemblyName);

app.Run();

Log.Information("Finalizando serviço {AssemblyName}", assemblyName);