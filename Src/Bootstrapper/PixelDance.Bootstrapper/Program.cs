using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PixelDance.Shared.Infrastructure.Bootstrapper;

namespace PixelDance.Bootstrapper
{
    public class Program
    {
        public static Task Main(string[] args)
            => CreateHostBuilder(args)
                .Build()
                .RunAsync();

        private static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .UserCustomLogging()
                .ConfigureWebHostDefaults(webBuilder 
                    => webBuilder.UseStartup<Startup>());
    }
}
