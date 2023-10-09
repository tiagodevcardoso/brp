using BRL.Infrastructure.Data;
using Serilog;
using Serilog.Events;

namespace BRP.Services.API.Person.Extensions
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
    }
}