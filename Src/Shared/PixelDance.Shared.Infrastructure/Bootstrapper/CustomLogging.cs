using Serilog;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PixelDance.Bootstrapper")]
namespace PixelDance.Shared.Infrastructure.Bootstrapper
{
    internal static class CustomLogging
    {
        public static IHostBuilder UserCustomLogging(this IHostBuilder builder)
            => builder
                .UseSerilog((context, configuration) => {
                    configuration
                        .Enrich.FromLogContext()
                        .Enrich.WithProperty("Envirnoment", context.HostingEnvironment.EnvironmentName)
                        .WriteTo.Console()
                        .ReadFrom.Configuration(context.Configuration);
                });
    }
}
