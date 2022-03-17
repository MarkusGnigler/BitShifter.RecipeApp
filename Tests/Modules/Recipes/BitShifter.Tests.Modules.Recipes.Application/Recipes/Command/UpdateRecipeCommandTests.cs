using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Xunit;
using FluentAssertions;

using BitShifter.Shared.Kernel.Enums;
using BitShifter.Shared.Kernel.Exceptions;
using BitShifter.Modules.Recipes.Domain.Entities;
using BitShifter.Tests.Modules.Recipes.Application.Fixtures;
using BitShifter.Modules.Recipes.Application.Recipes;
using BitShifter.Modules.Recipes.Application.Recipes.Command.UpdateRecipe;

namespace BitShifter.Tests.Modules.Recipes.Application.Recipes.Command
{
    public class UpdateRecipeCommandHandlerTests : IntegrationTestBase
    {
        public UpdateRecipeCommandHandlerTests(
            InMemoryDbContextFixture contextFixture,
            MappingFixture mappingFixture)
                : base(contextFixture, mappingFixture)
        { }

        [Fact]
        public void CreateHandler_Succedded()
        {
            // Arrange
            var sut = new UpdateRecipeHandler(mapper, recipeRepository, categoryRepository);

            // Act

            // Assert
            sut.Should()
                .NotBeNull();
        }

        [Fact]
        public async Task UpdateRecipeCommand_Succedded()
        {
            // Arrange
            var recipe = await context.Recipes
                .Include(x => x.Category)
                .FirstAsync(x => x.Slug == "test-recipe");

            var command = new UpdateRecipeCommand()
            {
                Id = recipe.Id,
                Img = "updated-recipe-img.jpg",
                Slug = "updated-recipe-slug",
                Title = "updated-recipe-title",
                Preparation = "updated-recipe-preparation",
                Description = "updated-recipe-description",
                Liked = true,
                Position = 42,
                Priority = (int)PriorityLevel.High,
                CategoryId = recipe.Category.Id,
                Ingredients = mapper.Map<IEnumerable<IngredientsDto>>(
                    recipe.Ingredients).ToArray()
            };

            var sut = new UpdateRecipeHandler(mapper, recipeRepository, categoryRepository);

            // Act
            var result = await sut.Handle(command, default);

            // Assert
            result.Slug.Should()
                .Be("updated-recipe-slug");
            result.Title.Should()
                .Be("updated-recipe-title");
            result.Img.Should()
                .Be("updated-recipe-img.jpg");
            result.Preparation.Should()
                .Be("updated-recipe-preparation");
            result.Description.Should()
                .Be("updated-recipe-description");
            result.Liked.Should()
                .BeTrue();
            result.Position.Should()
                .Be(42);
            result.Priority.Should()
                .Be(PriorityLevel.High);
            result.Category.Should()
                .Be(recipe.Category.Name);
        }

        [Fact]
        public async Task UpdateRecipeCommand_WithNewCategory_Succedded()
        {
            // Arrange
            var recipe = await context.Recipes
                .Include(x => x.Category)
                .FirstAsync(x => x.Slug == "test-recipe");

            var category = new Category("new-updated-category");
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            var command = new UpdateRecipeCommand()
            {
                Id = recipe.Id,
                Img = recipe.Img,
                Slug = recipe.Slug,
                Title = recipe.Title,
                Preparation = recipe.Preparation,
                Description = recipe.Description,
                Liked = recipe.Liked,
                Position = recipe.Position,
                Priority = (int)recipe.Priority,
                CategoryId = category.Id,
                Ingredients = mapper.Map<IEnumerable<IngredientsDto>>(
                    recipe.Ingredients).ToArray()
            };

            var sut = new UpdateRecipeHandler(mapper, recipeRepository, categoryRepository);

            // Act
            var result = await sut.Handle(command, default);

            // Assert
            result.Category.Should()
                .Be("new-updated-category");
        }

        [Fact]
        public async Task UpdateRecipeCommand_WithIngredients_Succedded()
        {
            // Arrange
            var recipe = await context.Recipes
                .Include(x => x.Category)
                .FirstAsync(x => x.Slug == "test-recipe");

            var ingredients = mapper.Map<IEnumerable<IngredientsDto>>(
                    new[]
                    {
                        Ingredient.Create("updated-ingredient1", 42, "updated-unit1"),
                        Ingredient.Create("updated-ingredient2", 42, "updated-unit2")
                    }).ToArray();

            var command = new UpdateRecipeCommand()
            {
                Id = recipe.Id,
                Img = recipe.Img,
                Slug = recipe.Slug,
                Title = recipe.Title,
                Preparation = recipe.Preparation,
                Description = recipe.Description,
                Liked = recipe.Liked,
                Position = recipe.Position,
                Priority = (int)recipe.Priority,
                CategoryId = recipe.Category.Id,
                Ingredients = ingredients
            };

            var sut = new UpdateRecipeHandler(mapper, recipeRepository, categoryRepository);

            // Act
            var result = await sut.Handle(command, default);

            // Assert
            result.Ingredients.Should()
                .NotBeNullOrEmpty();

            result.Ingredients.First().Title.Should()
                .Be(ingredients.First().Title);
            result.Ingredients.First().Quantity.Should()
                .Be(ingredients.First().Quantity);
            result.Ingredients.First().Unit.Should()
                .Be(ingredients.First().Unit);

            result.Ingredients.Last().Title.Should()
                .Be(ingredients.Last().Title);
            result.Ingredients.Last().Quantity.Should()
                .Be(ingredients.Last().Quantity);
            result.Ingredients.Last().Unit.Should()
                .Be(ingredients.Last().Unit);
        }

        [Fact]
        public async Task UpdateRecipeCommand_Throws_RecipeNotFoundException()
        {
            // Arrange
            var recipe = await context.Recipes
                .Include(x => x.Category)
                .FirstAsync(x => x.Slug == "test-recipe");

            var command = new UpdateRecipeCommand()
            {
                Id = Guid.Empty,

                Img = recipe.Img,
                Slug = recipe.Slug,
                Title = recipe.Title,
                Preparation = recipe.Preparation,
                Description = recipe.Description,
                Liked = false,
                Position = 1,
                Priority = (int)PriorityLevel.None,
                CategoryId = recipe.Category.Id,
                Ingredients = new List<IngredientsDto>()
            };

            var sut = new UpdateRecipeHandler(mapper, recipeRepository, categoryRepository);

            // Act
            Func<Task> result = async () => await sut.Handle(command, default);

            // Assert
            (await result.Should()
                .ThrowAsync<EntityNotFoundException>())
                .WithMessage($"No recipe with id \"{command.Id}\" found.");
        }

        [Fact]
        public async Task UpdateRecipeCommand_Throws_CategoryNotFoundException()
        {
            // Arrange
            var recipe = await context.Recipes
                .Include(x => x.Category)
                .FirstAsync(x => x.Slug == "test-recipe");

            var command = new UpdateRecipeCommand()
            {
                CategoryId = Guid.Empty,

                Id = recipe.Id,
                Img = recipe.Img,
                Slug = recipe.Slug,
                Title = recipe.Title,
                Preparation = recipe.Preparation,
                Description = recipe.Description,
                Liked = false,
                Position = 1,
                Priority = (int)PriorityLevel.None,
                Ingredients = new List<IngredientsDto>()
            };

            var sut = new UpdateRecipeHandler(mapper, recipeRepository, categoryRepository);

            // Act
            Func<Task> result = async () => await sut.Handle(command, default);

            // Assert
            (await result.Should()
                .ThrowAsync<EntityNotFoundException>())
                .WithMessage($"No category with id \"{command.CategoryId}\" found.");
        }
    }
}
