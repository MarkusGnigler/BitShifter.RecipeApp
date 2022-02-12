using System;
using System.Threading.Tasks;

using Xunit;
using FluentAssertions;

using PixelDance.Shared.Kernel.Exceptions;
using PixelDance.Tests.Modules.Recipes.Application.Fixtures;
using PixelDance.Modules.Recipes.Application.Recipes.Queries.GetRecipeBySlug;

namespace PixelDance.Tests.Modules.Recipes.Application.Recipes.Queries
{
    public class GetRecipeBySlugHandlerTests : IntegrationTestBase
    {
        public GetRecipeBySlugHandlerTests(
            InMemoryDbContextFixture contextFixture,
            MappingFixture mappingFixture)
                : base(contextFixture, mappingFixture)
        { }

        [Fact]
        public void CreateHandler_Succedded()
        {
            // Arrange
            var sut = new GetRecipeBySlugHandler(mapper, recipeRepository);

            // Act

            // Assert
            sut.Should()
                .NotBeNull();
        }

        [Fact]
        public async Task GetRecipeBySlug_Succedded()
        {
            // Arrange
            string expectedRecipeSlug = "test-recipe";

            var query = new GetRecipeBySlugQuery(expectedRecipeSlug);
            var sut = new GetRecipeBySlugHandler(mapper, recipeRepository);

            // Act
            var result = await sut.Handle(query, default);

            // Assert
            result.Slug.Should()
                .Be(expectedRecipeSlug);
        }

        [Fact]
        public async Task GetRecipeBySlug_Throws_NotFoundException()
        {
            // Arrange
            string expectedRecipeSlug = "abc test-recipe";

            var query = new GetRecipeBySlugQuery(expectedRecipeSlug);
            var sut = new GetRecipeBySlugHandler(mapper, recipeRepository);

            // Act
            Func<Task> result = async () => await sut.Handle(query, default);

            // Assert
            (await result.Should()
                .ThrowAsync<EntityNotFoundException>())
                .WithMessage($"Es wurde kein Rezept mit der URL \"{expectedRecipeSlug}\" gefunden.");
        }
    }
}
