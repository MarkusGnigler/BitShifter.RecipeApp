using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PixelDance.Shared.Abstractions.EfCore.Seeder;
using PixelDance.Modules.Recipes.Infrastructure.Persistence.Seeding;

namespace PixelDance.Modules.Recipes.Infrastructure.Persistence
{
    internal class ContextSeeder : ISeeder
    {
        public async Task SeedSampleData(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();

            await context.Database.EnsureCreatedAsync();

            await SeedSampleDataAsync(context);
        }

        private static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            await CategorySeeder.SeedCategory(context);
            await RecipeSeeder.SeedRecipe(context);
        }
    }
}
