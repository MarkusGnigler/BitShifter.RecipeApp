using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BitShifter.Bootstrapper")]
namespace BitShifter.Shared.Infrastructure.Bootstrapper
{
    internal static class CorsMiddleware
    {
        private const string CORS_NAME = "BsGatewayPolicy";

        public static IServiceCollection AddBsCors(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedOrigins = configuration
                .GetValue<string>("AllowedHosts")
                .Split(',') ?? new[] { "*" };

            services.AddCors(o => o.AddPolicy(CORS_NAME, builder =>
                   builder
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .SetIsOriginAllowed((host) => true)
                       //.AllowCredentials()
                       .WithOrigins(allowedOrigins)
               ));

            return services;
        }

        public static IApplicationBuilder UsePxdCors(this IApplicationBuilder app)
        {
            app.UseCors(CORS_NAME);

            return app;
        }
    }
}
