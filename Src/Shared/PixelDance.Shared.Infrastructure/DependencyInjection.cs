using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PixelDance.Shared.Infrastructure.EfCore;
using PixelDance.Shared.Infrastructure.MediatR;
using PixelDance.Shared.Infrastructure.Mapping;
using PixelDance.Shared.Infrastructure.Services;
using PixelDance.Shared.Infrastructure.Utilities;

[assembly:InternalsVisibleTo("PixelDance.Bootstrapper")]
namespace PixelDance.Shared.Infrastructure
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddMapping()
                .AddMediatR()
                .AddCoreServices()
                .AddEfCore();

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            ServiceActivator.Configure(app.ApplicationServices);

            app.UseEfCore();

            return app;
        }
    }
}
