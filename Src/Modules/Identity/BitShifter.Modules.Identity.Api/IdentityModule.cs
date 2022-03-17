using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BitShifter.Modules.Identity.Core;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("BitShifter.Bootstrapper")]
namespace BitShifter.Modules.Identity.Api
{
    internal static class IdentityModule
    {
        public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore(configuration);

            return services;
        }

        public static IApplicationBuilder UseIdentityModule(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}