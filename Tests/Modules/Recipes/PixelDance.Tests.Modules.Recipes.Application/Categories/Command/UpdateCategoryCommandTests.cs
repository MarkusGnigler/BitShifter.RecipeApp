using System;
using System.Threading.Tasks;

using Xunit;
using FluentAssertions;

using Microsoft.EntityFrameworkCore;
using PixelDance.Shared.Kernel.Exceptions;
using PixelDance.Tests.Modules.Recipes.Application.Fixtures;
using PixelDance.Modules.Recipes.Application.Categories.Command.UpdateCategory;

namespace PixelDance.Tests.Modules.Recipes.Application.Categories.Command
{
    public class UpdateCategoryCommandTests : IntegrationTestBase
    {
        public UpdateCategoryCommandTests(
            InMemoryDbContextFixture contextFixture,
            MappingFixture mappingFixture)
                : base(contextFixture, mappingFixture)
        { }

        [Fact]
        public void CreateHandler_Succedded()
        {
            // Arrange
            var sut = new UpdateCategoryCommand(mapper, categoryRepository);

            // Act

            // Assert
            sut.Should()
                .NotBeNull();
        }

        [Fact]
        public async Task UpdateCategory_Succedded()
        {
            // Arrange
            string expectedCategoryName = "upated-category";

            var category = await context.Categories
                .FirstAsync(x => x.Name == "test-category");

            var query = new UpdateCategory()
            {
                Id = category.Id,
                Name = expectedCategoryName
            };
            var sut = new UpdateCategoryCommand(mapper, categoryRepository);

            // Act
            var result = await sut.Handle(query, default);

            // Assert
            result.Name.Should()
                .Be(expectedCategoryName);
        }

        [Fact]
        public async Task UpdateCategory_Throws_NotFoundException()
        {
            // Arrange
            var query = new UpdateCategory()
            {
                Id = Guid.Empty,
            };
            var sut = new UpdateCategoryCommand(mapper, categoryRepository);

            // Act
            Func<Task> result = async () => await sut.Handle(query, default);

            // Assert
            (await result.Should()
                .ThrowAsync<EntityNotFoundException>())
                .WithMessage($"No category with id \"{query.Id}\" found.");
        }
    }
}
