using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Xunit;
using FluentAssertions;

using PixelDance.Tests.Modules.Recipes.Application.Fixtures;
using PixelDance.Modules.Recipes.Application.Categories.Command.CreateCategory;

namespace PixelDance.Tests.Modules.Recipes.Application.Categories.Command
{
    public class CreateCategoryCommandTests : IntegrationTestBase
    {
        public CreateCategoryCommandTests(
            InMemoryDbContextFixture contextFixture,
            MappingFixture mappingFixture)
                : base(contextFixture, mappingFixture)
        { }

        [Fact]
        public void CreateHandler_Succedded()
        {
            // Arrange
            var sut = new CreateCategoryCommand(mapper, categoryRepository);

            // Act

            // Assert
            sut.Should()
                .NotBeNull();
        }

        [Fact]
        public async Task CreateCategoryCommand_Succedded()
        {
            // Arrange
            var query = new CreateCategory()
            {
                Id = Guid.NewGuid(),
                Name = "created-recipe"
            };
            var sut = new CreateCategoryCommand(mapper, categoryRepository);

            // Act
            var result = await sut.Handle(query, default);

            // Assert
            result.Name.Should()
                .Be("created-recipe");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task CreateCategoryCommand_Throws_ArgumentException_IfNameIsInvalid(string categoryName)
        {
            // Arrange
            var query = new CreateCategory()
            {
                Id = Guid.NewGuid(),
                Name = categoryName
            };
            var sut = new CreateCategoryCommand(mapper, categoryRepository);

            // Act
            Func<Task> result = async () => await sut.Handle(query, default);

            // Assert
            (await result.Should()
                .ThrowAsync<InvalidOperationException>())
                .WithMessage("Category empty name not allowed.");
        }

        [Fact]
        public async Task CreateCategoryCommand_Throws_ArgumentException_If_CategoryExists()
        {
            // Arrange
            var category = await context.Categories
                .FirstAsync();

            var query = new CreateCategory()
            {
                Id = category.Id,
                Name = category.Name
            };
            var sut = new CreateCategoryCommand(mapper, categoryRepository);

            // Act
            Func<Task> result = async () => await sut.Handle(query, default);

            // Assert
            (await result.Should()
                .ThrowAsync<ArgumentException>())
                .WithMessage("Category allready exists");
        }
    }
}
