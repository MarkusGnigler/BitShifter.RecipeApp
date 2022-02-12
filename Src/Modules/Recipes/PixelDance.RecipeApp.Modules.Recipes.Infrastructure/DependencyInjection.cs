using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using PixelDance.Shared.Infrastructure.EfCore;
using PixelDance.Modules.Recipes.Domain.Entities;
using PixelDance.Modules.Recipes.Infrastructure.Persistence;
using PixelDance.Modules.Recipes.Application.Common.Interfaces;

namespace PixelDance.Modules.Recipes.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<ApplicationDbContext>()
                .AddAggregateRepository<ApplicationDbContext, Recipe>()
                .AddAggregateRepository<ApplicationDbContext, Category>();

            services.AddScoped<IApplicationDbContext>(
                provider => provider.GetService<ApplicationDbContext>());

            return services;
        }
    }
}
