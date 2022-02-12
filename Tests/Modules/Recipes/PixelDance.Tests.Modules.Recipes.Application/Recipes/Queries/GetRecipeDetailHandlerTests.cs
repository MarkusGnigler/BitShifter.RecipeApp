using System;
using System.Threading.Tasks;

using Moq;
using Xunit;
using FluentAssertions;

using PixelDance.Modules.Recipes.Domain.Entities;
using PixelDance.Shared.Kernel.Exceptions;
using PixelDance.Tests.Modules.Recipes.Application.Fixtures;
using PixelDance.Modules.Recipes.Application.Recipes.Queries.GetRecipeDetail;

namespace PixelDance.Tests.Modules.Recipes.Application.Recipes.Queries
{
    public class GetRecipeDetailHandlerTests : IntegrationTestBase
    {
        public GetRecipeDetailHandlerTests(
            InMemoryDbContextFixture contextFixture,
            MappingFixture mappingFixture)
                : base(contextFixture, mappingFixture)
        { }

        [Fact]
        public void CreateHandler_Succedded()
        {
            // Arrange
            var sut = new GetRecipeDetailHandler(mapper, recipeRepository);

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
            var recipeSeed = new Recipe(
                "test-recipe", "Test Recipe", "test-recipe-image.png",
                "test perparation", "test description", category);

            await recipeRepository.AddAsync(recipeSeed);
            await recipeRepository.SaveChangesAsync();

            var query = new GetRecipeDetailQuery(recipeSeed.Id);
            var sut = new GetRecipeDetailHandler(mapper, recipeRepository);

            // Act
            //await repository.AddAsync(recipeSeed);
            //await repository.SaveChangesAsync();

            var result = await sut.Handle(query, default);

            // Assert
            result.Slug.Should()
                .Be("test-recipe");
        }

        [Fact]
        public async Task GetRecipeDetailQuery_Throws_NotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var query = new GetRecipeDetailQuery(id);
            var sut = new GetRecipeDetailHandler(mapper, recipeRepository);

            // Act
            Func<Task> result = async () => await sut.Handle(query, default);

            // Assert
            (await result.Should()
                .ThrowAsync<EntityNotFoundException>())
                .WithMessage($"Es wurde kein Rezept mit der Id \"{id}\" gefunden.");
        }
    }
}
