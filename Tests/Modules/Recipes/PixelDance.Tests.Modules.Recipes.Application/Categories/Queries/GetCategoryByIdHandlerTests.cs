using System;
using System.Threading.Tasks;

using Xunit;
using FluentAssertions;

using PixelDance.Shared.Kernel.Exceptions;
using PixelDance.Modules.Recipes.Domain.Entities;
using PixelDance.Modules.Recipes.Application.Categories.Queries.GetCategoryById;
using PixelDance.Tests.Modules.Recipes.Application.Fixtures;

namespace PixelDance.Tests.Modules.Recipes.Application.Categories.Queries
{
    public class GetCategoryByIdHandlerTests : IntegrationTestBase
    {
        public GetCategoryByIdHandlerTests(
            InMemoryDbContextFixture contextFixture,
            MappingFixture mappingFixture)
                : base(contextFixture, mappingFixture)
        { }

        [Fact]
        public void CreateHandler_Succedded()
        {
            // Arrange
            var sut = new GetCategoryByIdHandler(mapper, categoryRepository);

            // Act

            // Assert
            sut.Should()
                .NotBeNull();
        }

        [Fact]
        public async Task GetCategoryList_Succedded()
        {
            // Arrange
            var category = new Category("category-to-read");
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            var query = new GetCategoryByIdQuery()
            {
                Id = category.Id
            };
            var sut = new GetCategoryByIdHandler(mapper, categoryRepository);

            // Act
            var result = await sut.Handle(query, default);

            // Assert
            result.Id.Should()
                .Be(category.Id);
            result.Name.Should()
                .Be(category.Name);
        }

        [Fact]
        public async Task GetCategoryList_Thros_NotFoundException()
        {
            // Arrange
            var query = new GetCategoryByIdQuery()
            {
                Id = Guid.Empty
            };
            var sut = new GetCategoryByIdHandler(mapper, categoryRepository);

            // Act
            Func<Task> result = async () => await sut.Handle(query, default);

            // Assert
            (await result.Should()
                .ThrowAsync<EntityNotFoundException>())
                .WithMessage($"Keine Kategorie mit der Id \"{query.Id}\" gefunden.");
        }
    }
}
