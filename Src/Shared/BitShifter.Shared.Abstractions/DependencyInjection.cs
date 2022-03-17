using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("BitShifter.Bootstrapper")]
namespace BitShifter.Shared.Abstractions
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
