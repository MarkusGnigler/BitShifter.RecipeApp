using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelDance.Modules.Identity.Core;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("PixelDance.Bootstrapper")]
namespace PixelDance.Modules.Identity.Api
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