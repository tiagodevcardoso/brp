using MassTransit;
using Serilog.Events;
using Serilog;

namespace BRP.Services.Publish.MassTransit.Generic.Extensions
{
    public static class AppBuilderExtension
    {
        public static void ConfigureLogs(this WebApplicationBuilder builder, string seqUri)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Seq(seqUri)
                .WriteTo.Console()
                .CreateLogger();
            builder.Host.UseSerilog();
        }

        public static void ConfigureHealthChecks(this WebApplicationBuilder builder)
        {
            var rabbitMqUri = builder.Configuration["rabbitmq_uri"];
            builder.Services.AddHealthChecks().AddRabbitMQ(rabbitConnectionString: rabbitMqUri);
        }

        public static void AddMassTransitPublisher(this IServiceCollection services, IConfiguration configuration, string rabbitMqUri)
        {
            services.AddMassTransit(bus =>
            {
                bus.UsingRabbitMq((ctx, busConfigurator) =>
                {
                    busConfigurator.Host(rabbitMqUri);
                });
            });

            services.Configure<MassTransitHostOptions>(options =>
            {
                options.WaitUntilStarted = true;
                options.StartTimeout = TimeSpan.FromSeconds(30);
                options.StopTimeout = TimeSpan.FromMinutes(1);
            });

            services.AddMassTransitHostedService();
        }
    }
}