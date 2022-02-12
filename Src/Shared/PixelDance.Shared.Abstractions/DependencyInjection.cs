using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PixelDance.Shared.Abstractions.Mapping;

[assembly:InternalsVisibleTo("PixelDance.Bootstrapper")]
namespace PixelDance.Shared.Abstractions
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddAbstractions(this IServiceCollection services)
        {
            return services;
        }

        public static IApplicationBuilder UseAbstractions(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
