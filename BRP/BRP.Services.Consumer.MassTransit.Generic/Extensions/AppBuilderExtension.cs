using Serilog;
using Serilog.Events;

namespace BRP.Services.Consumer.MassTransit.Generic.Extensions
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
    }
}
