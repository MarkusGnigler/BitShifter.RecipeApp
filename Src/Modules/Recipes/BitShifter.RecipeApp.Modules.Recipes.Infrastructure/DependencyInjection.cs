using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using BitShifter.Shared.Infrastructure.EfCore;
using BitShifter.Modules.Recipes.Domain.Entities;
using BitShifter.Modules.Recipes.Infrastructure.Persistence;
using BitShifter.Modules.Recipes.Application.Common.Interfaces;

namespace BitShifter.Modules.Recipes.Infrastructure
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
