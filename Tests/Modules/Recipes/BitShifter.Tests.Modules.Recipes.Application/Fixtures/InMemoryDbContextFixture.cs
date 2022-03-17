using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Xunit;

using BitShifter.Modules.Recipes.Infrastructure.Persistence;
using BitShifter.Modules.Recipes.Domain.Entities;

namespace BitShifter.Tests.Modules.Recipes.Application.Fixtures
{
    public class InMemoryDbContextFixture : IAsyncLifetime
    {
        internal ApplicationDbContext? Context { get; private set; }

        public async Task InitializeAsync()
        {
            Context = CreatDbContext();

            await Seed();

            await Context.Database.EnsureCreatedAsync();
        }

        public async Task DisposeAsync()
        {
            if (Context is null) return;

            await Context.Database.EnsureDeletedAsync();

            await Context.DisposeAsync();
        }

        private static ApplicationDbContext CreatDbContext()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new();

            optionsBuilder.UseInMemoryDatabase("PixelDance IntegrationTests");

            return new(optionsBuilder.Options);
        }

        private async Task Seed()
        {
            if (Context is null) return;

            if (await Context.Categories.AnyAsync()) return;

            var category = new Category("test-category");

            await Context.Categories.AddAsync(category);
            await Context.SaveChangesAsync();

            if (await Context.Recipes.AnyAsync()) return;

            var recipe = new Recipe(
                "test-recipe", "Test Recipe", "test-recipe-image.png",
                "test perparation", "test description", category);

            recipe.UpdateIngredients(new[] { Ingredient.Create("test-ingredient", 6.6, "test-unit") });

            await Context.Recipes.AddAsync(recipe);
            await Context.SaveChangesAsync();
        }
    }
}
