using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelDance.Modules.Recipes.Application;
using PixelDance.Modules.Recipes.Infrastructure;

[assembly:InternalsVisibleTo("PixelDance.Bootstrapper")]
namespace PixelDance.Modules.Recipes.Api
{
    internal static class RecipesModule
    {
        public static IServiceCollection AddRecipesModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddApplication(configuration)
                .AddInfrastructure(configuration);

            return services;
        }

        public static IApplicationBuilder UseRecipesModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}