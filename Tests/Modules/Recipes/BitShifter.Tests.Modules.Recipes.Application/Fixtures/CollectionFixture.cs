using Xunit;

namespace BitShifter.Tests.Modules.Recipes.Application.Fixtures
{
    [CollectionDefinition(nameof(CollectionFixture))]
    public class CollectionFixture
        : ICollectionFixture<InMemoryDbContextFixture>, ICollectionFixture<MappingFixture>
    { }
}
