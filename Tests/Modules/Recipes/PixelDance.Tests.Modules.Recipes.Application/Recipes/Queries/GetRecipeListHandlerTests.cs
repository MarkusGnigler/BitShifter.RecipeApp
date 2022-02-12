using System.Linq;
using System.Threading.Tasks;

using Xunit;
using FluentAssertions;

using PixelDance.Modules.Recipes.Domain.Entities;
using PixelDance.Tests.Modules.Recipes.Application.Fixtures;
using PixelDance.Modules.Recipes.Application.Recipes.Queries.GetRecipeList;

namespace PixelDance.Tests.Modules.Recipes.Application.Recipes.Queries
{
    public class GetRecipeListHandlerTests : IntegrationTestBase
    {
        public GetRecipeListHandlerTests(
            InMemoryDbContextFixture contextFixture,
            MappingFixture mappingFixture)
                : base(contextFixture, mappingFixture)
        { }

        [Fact]
        public void CreateHandler_Succedded()
        {
            // Arrange
            var sut = new GetRecipeListHandler(mapper, recipeRepository);

            // Act

            // Assert
            sut.Should()
                .NotBeNull();
        }

        [Fact]
        public async Task GetRecipeDetail_Succedded()
        {
            // Arrange
            var category = new Category("test-category");
            var recipeSeed1 = new Recipe(
                "test-recipe1", "Test Recipe1", "test-recipe-image1.png",
                "test perparation1", "test description1", category);
            var recipeSeed2 = new Recipe(
                "test-recipe2", "Test Recipe2", "test-recipe-image2.png",
                "test perparation2", "test description2", category);

            var query = new GetRecipeListQuery();
            var sut = new GetRecipeListHandler(mapper, recipeRepository);

            // Act
            await recipeRepository.AddAsync(recipeSeed1);
            await recipeRepository.AddAsync(recipeSeed2);
            await recipeRepository.SaveChangesAsync();

            var result = await sut.Handle(query, default);

            // Assert
            result.Recipes.Count().Should()
                .Be(3);
            result.Recipes.Should()
                .NotBeNullOrEmpty();
            result.Recipes.Last().Title.Should()
                .Be(recipeSeed2.Title);
        }
    }
}
