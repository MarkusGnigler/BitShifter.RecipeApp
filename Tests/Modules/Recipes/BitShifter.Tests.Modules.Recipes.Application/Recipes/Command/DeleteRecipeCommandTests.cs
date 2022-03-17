using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Moq;
using Xunit;
using FluentAssertions;

using BitShifter.Shared.Abstractions.Interfaces;
using BitShifter.Shared.Kernel.Exceptions;
using BitShifter.Modules.Recipes.Domain.Entities;
using BitShifter.Tests.Modules.Recipes.Application.Fixtures;
using BitShifter.Modules.Recipes.Application.Recipes.Command.DeleteRecipe;

namespace BitShifter.Tests.Modules.Recipes.Application.Recipes.Command
{
    public class DeleteRecipeCommandHandlerTests : IntegrationTestBase
    {
        private readonly Mock<IWebRootWatcher> rootWatcherStub = new();

        public DeleteRecipeCommandHandlerTests(
            InMemoryDbContextFixture contextFixture,
            MappingFixture mappingFixture)
                : base(contextFixture, mappingFixture)
        { }

        [Fact]
        public void CreateHandler_Succedded()
        {
            // Arrange
            var sut = new DeleteRecipeHandler(recipeRepository, rootWatcherStub.Object);

            // Act

            // Assert
            sut.Should()
                .NotBeNull();
        }

        [Fact]
        public async Task DeleteRecipeCommand_Succedded()
        {
            // Arrange
            var recipe = await context.Recipes.FirstAsync(x => x.Slug == "test-recipe");

            var command = new DeleteRecipeCommand()
            {
                Id = recipe.Id
            };
            var sut = new DeleteRecipeHandler(recipeRepository, rootWatcherStub.Object);

            // Act
            var result = await sut.Handle(command, default);

            //var getBySlugSpec = new RecipeBySlugSpec(command.Slug);
            var deletedReciupe = await recipeRepository.GetByIdAsync(recipe.Id);

            // Assert
            deletedReciupe.Should()
                .BeNull();
        }

        [Fact]
        public async Task DeleteRecipeCommand_Throws_RecipeNotFoundException()
        {
            // Arrange
            var command = new DeleteRecipeCommand()
            {
                Id = Guid.Empty,
            };
            var sut = new DeleteRecipeHandler(recipeRepository, rootWatcherStub.Object);

            // Act
            Func<Task> result = async () => await sut.Handle(command, default);

            // Assert
            (await result.Should()
                .ThrowAsync<EntityNotFoundException>())
                .WithMessage($"No recipe with id \"{command.Id}\" found.");
        }

        [Fact]
        public async Task DeleteRecipeCommand_Invoke_ImageDeleteFunction()
        {
            // Arrange
            var recipe = await context.Recipes
                .FirstAsync(x => x.Slug == "test-recipe");

            var command = new DeleteRecipeCommand()
            {
                Id = recipe.Id,
            };
            var sut = new DeleteRecipeHandler(recipeRepository, rootWatcherStub.Object);

            rootWatcherStub.Setup(x => x.RemoveFileAsync(recipe.Img))
                .Returns(() => Task.CompletedTask);

            // Act
            var result = await sut.Handle(command, default);

            // Assert
            rootWatcherStub.Verify(x => x.RemoveFileAsync(recipe.Img), Times.Once);
        }

        [Fact]
        public async Task DeleteRecipeCommand_NotInvoke_ImageDeleteFunction()
        {
            // Arrange
            var category = new Category("image-category");
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            var duplicateImagePathRecipe = new Recipe(
                "duplicate-image-recipe", "duplicate-image Recipe", "test-recipe-image.png",
                "duplicate-image perparation", "duplicate-image description", category);
            await context.Recipes.AddAsync(duplicateImagePathRecipe);
            await context.SaveChangesAsync();

            var recipe = await context.Recipes
                .FirstAsync(x => x.Slug == "test-recipe");

            var command = new DeleteRecipeCommand()
            {
                Id = recipe.Id,
            };
            var sut = new DeleteRecipeHandler(recipeRepository, rootWatcherStub.Object);

            rootWatcherStub.Setup(x => x.RemoveFileAsync(recipe.Img))
                .Returns(() => Task.CompletedTask);

            // Act
            var result = await sut.Handle(command, default);

            // Assert
            rootWatcherStub.Verify(x => x.RemoveFileAsync(recipe.Img), Times.Never);
        }
    }
}
