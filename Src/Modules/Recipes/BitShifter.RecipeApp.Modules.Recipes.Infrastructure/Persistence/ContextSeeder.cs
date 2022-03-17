using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using BitShifter.Shared.Abstractions.EfCore.Seeder;
using BitShifter.Modules.Recipes.Infrastructure.Persistence.Seeding;

namespace BitShifter.Modules.Recipes.Infrastructure.Persistence
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
