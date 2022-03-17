using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Xunit;
using FluentAssertions;

using BitShifter.Shared.Kernel.Exceptions;
using BitShifter.Modules.Recipes.Domain.Entities;
using BitShifter.Modules.Recipes.Domain.Exceptions;
using BitShifter.Modules.Recipes.Domain.Specifications;
using BitShifter.Tests.Modules.Recipes.Application.Fixtures;
using BitShifter.Modules.Recipes.Application.Recipes;
using BitShifter.Modules.Recipes.Application.Recipes.Command.CreateRecipe;

namespace BitShifter.Tests.Modules.Recipes.Application.Recipes.Command
{
    public class CreateRecipeHandlerTests : IntegrationTestBase
    {
        public CreateRecipeHandlerTests(
            InMemoryDbContextFixture contextFixture,
            MappingFixture mappingFixture)
                : base(contextFixture, mappingFixture)
        { }

        [Fact]
        public void CreateHandler_Succedded()
        {
            // Arrange
            var sut = new CreateRecipeHandler(mapper, recipeRepository, categoryRepository);

            // Act

            // Assert
            sut.Should()
                .NotBeNull();
        }

        [Fact]
        public async Task CreateRecipeCommand_Succedded()
        {
            // Arrange
            string expectedRecipeSlug = "creation recipe";
            string expectedIngredientTitle = "creation ingredient";

            var category = await context.Categories
                .FirstAsync(x => x.Name == "test-category");

            var command = new CreateRecipeCommand()
            {
                Id = Guid.Empty,
                Img = "creation-recipe-image.png",
                Slug = expectedRecipeSlug,
                Title = "creation Recipe",
                Preparation = "creation perparation",
                Description = "creation description",
                CategoryId = category.Id,
                Ingredients = mapper.Map<IEnumerable<IngredientsDto>>(
                    new[] { Ingredient.Create(expectedIngredientTitle, 6.6, "test-unit") }).ToArray()
            };
            var sut = new CreateRecipeHandler(mapper, recipeRepository, categoryRepository);

            // Act
            var result = await sut.Handle(command, default);

            var getBySlugSpec = new RecipeBySlugSpec(command.Slug);
            await recipeRepository.GetBySpecAsync(getBySlugSpec);

            // Assert
            result.Slug.Should()
                .Be(expectedRecipeSlug);
            result.Ingredients.First().Title.Should()
                .Be(expectedIngredientTitle);
        }

        [Fact]
        public async Task CreateRecipeCommand_Throws_CategoryNotFoundException()
        {
            // Arrange
            var command = new CreateRecipeCommand()
            {
                CategoryId = Guid.Empty,

                Id = Guid.Empty,
                Img = string.Empty,
                Slug = string.Empty,
                Title = string.Empty,
                Preparation = string.Empty,
                Description = string.Empty,
                Ingredients = new List<IngredientsDto>()
            };
            var sut = new CreateRecipeHandler(mapper, recipeRepository, categoryRepository);

            // Act
            Func<Task> result = async () => await sut.Handle(command, default);

            // Assert
            (await result.Should()
                .ThrowAsync<EntityNotFoundException>())
                .WithMessage($"No category with id \"{Guid.Empty}\" found.");
        }

        [Fact]
        public async Task CreateRecipeCommand_Throws_DuplicateTitleException()
        {
            // Arrange
            var category = await context.Categories
                .FirstAsync(x => x.Name == "test-category");

            var command = new CreateRecipeCommand()
            {
                Title = "Test Recipe",
                CategoryId = category.Id,

                Id = Guid.Empty,
                Img = "test value",
                Slug = "test value",
                Preparation = "test value",
                Description = "test value",
                Ingredients = new List<IngredientsDto>()
            };
            var sut = new CreateRecipeHandler(mapper, recipeRepository, categoryRepository);

            // Act
            Func<Task> result = async () => await sut.Handle(command, default);

            // Assert
            (await result.Should()
                .ThrowAsync<RecipeTitleAlreadyInUseException>())
                .WithMessage($"Die Titel \"{command.Title}\" ist bereits vergeben.");
        }

        [Fact]
        public async Task CreateRecipeCommand_Throws_DuplicateSlugException()
        {
            // Arrange
            var category = await context.Categories
                .FirstAsync(x => x.Name == "test-category");

            var command = new CreateRecipeCommand()
            {
                Slug = "test-recipe",
                CategoryId = category.Id,

                Id = Guid.Empty,
                Img = "test value",
                Title = "test value",
                Preparation = "test value",
                Description = "test value",
                Ingredients = new List<IngredientsDto>()

            };
            var sut = new CreateRecipeHandler(mapper, recipeRepository, categoryRepository);

            // Act
            Func<Task> result = async () => await sut.Handle(command, default);

            // Assert
            (await result.Should()
                .ThrowAsync<RecipeSlugAlreadyInUseException>())
                .WithMessage($"Die Url \"{command.Slug}\" ist bereits vergeben.");
        }
    }
}
