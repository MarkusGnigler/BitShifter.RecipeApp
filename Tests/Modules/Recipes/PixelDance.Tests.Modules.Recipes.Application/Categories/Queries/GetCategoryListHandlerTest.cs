using System.Linq;
using System.Threading.Tasks;

using Xunit;
using FluentAssertions;

using PixelDance.Modules.Recipes.Domain.Entities;
using PixelDance.Tests.Modules.Recipes.Application.Fixtures;

namespace PixelDance.Modules.Recipes.Application.Categories.Queries.GetCategoryList
{
    public class GetCategoryListHandlerTest : IntegrationTestBase
    {
        public GetCategoryListHandlerTest(
            InMemoryDbContextFixture contextFixture,
            MappingFixture mappingFixture)
                : base(contextFixture, mappingFixture) 
        { }

        [Fact]
        public void CreateHandler_Succedded()
        {
            // Arrange
            var sut = new GetCategoryListHandler(mapper, categoryRepository);

            // Act

            // Assert
            sut.Should()
                .NotBeNull();
        }

        [Fact]
        public async Task GetCategoryList_Succedded()
        {
            // Arrange
            var category = new Category("category2");
            var query = new GetCategoryListQuery();
            var sut = new GetCategoryListHandler(mapper, categoryRepository);

            // Act
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            var result = await sut.Handle(query, default);

            // Assert
            result.Categories.Count().Should()
                .Be(2);

            result.Categories.First().Name.Should()
                .Be("category2");
            result.Categories.Last().Name.Should()
                .Be("test-category");
        }
    }
}
