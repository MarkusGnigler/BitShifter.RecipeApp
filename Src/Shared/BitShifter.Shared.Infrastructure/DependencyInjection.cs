using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using BitShifter.Shared.Infrastructure.EfCore;
using BitShifter.Shared.Infrastructure.MediatR;
using BitShifter.Shared.Infrastructure.Mapping;
using BitShifter.Shared.Infrastructure.Services;
using BitShifter.Shared.Infrastructure.Utilities;

[assembly:InternalsVisibleTo("BitShifter.Bootstrapper")]
namespace BitShifter.Shared.Infrastructure
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
