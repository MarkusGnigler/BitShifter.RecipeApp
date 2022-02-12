using System;
using System.Threading.Tasks;

using Xunit;
using FluentAssertions;

using Microsoft.EntityFrameworkCore;
using PixelDance.Modules.Recipes.Domain.Entities;
using PixelDance.Shared.Kernel.Exceptions;
using PixelDance.Tests.Modules.Recipes.Application.Fixtures;
using PixelDance.Modules.Recipes.Domain.Exceptions;
using PixelDance.Modules.Recipes.Application.Categories.Command.DeleteCategory;

namespace PixelDance.Tests.Modules.Recipes.Application.Categories.Command
{
    public class DeleteCategoryCommandTests : IntegrationTestBase
    {
        public DeleteCategoryCommandTests(
            InMemoryDbContextFixture contextFixture,
            MappingFixture mappingFixture)
                : base(contextFixture, mappingFixture)
        { }

        [Fact]
        public void CreateHandler_Succedded()
        {
            // Arrange
            var sut = new DeleteCategoryCommand(categoryRepository, recipeRepository);

            // Act

            // Assert
            sut.Should()
                .NotBeNull();
        }

        [Fact]
        public async Task DeleteCategory_Succedded()
        {
            // Arrange
            var category = new Category("delete-category");
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            var query = new DeleteCategory()
            {
                Id = category.Id
            };
            var sut = new DeleteCategoryCommand(categoryRepository, recipeRepository);

            // Act
            var result = await sut.Handle(query, default);

            // Assert
            result.Should()
                .Be(category.Id);
        }

        [Fact]
        public async Task DeleteCategory_Throws_NotFoundException()
        {
            // Arrange
            var query = new DeleteCategory()
            {
                Id = Guid.Empty
            };
            var sut = new DeleteCategoryCommand(categoryRepository, recipeRepository);

            // Act
            Func<Task> result = async () => await sut.Handle(query, default);

            // Assert
            (await result.Should()
                .ThrowAsync<EntityNotFoundException>())
                .WithMessage($"No category with id \"{query.Id}\" found.");
        }

        [Fact]
        public async Task DeleteCategory_UsedCategory_Throws_Exception()
        {
            // Arrange
            var category = await context.Categories
                .FirstAsync(x => x.Name == "test-category");

            var query = new DeleteCategory()
            {
                Id = category.Id
            };
            var sut = new DeleteCategoryCommand(categoryRepository, recipeRepository);

            // Act
            Func<Task> result = async () => await sut.Handle(query, default);

            // Assert
            (await result.Should()
                .ThrowAsync<CategoryIsAlreadyInUseException>())
                .WithMessage($"Categorie \"{category.Name}\" ist noch in Verwendung.");
        }
    }
}
