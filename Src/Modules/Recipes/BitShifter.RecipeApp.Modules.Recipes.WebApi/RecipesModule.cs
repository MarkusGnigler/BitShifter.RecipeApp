using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BitShifter.Modules.Recipes.Application;
using BitShifter.Modules.Recipes.Infrastructure;

[assembly:InternalsVisibleTo("BitShifter.Bootstrapper")]
namespace BitShifter.Modules.Recipes.Api
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