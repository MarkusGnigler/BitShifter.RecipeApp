using System.Threading.Tasks;

using Xunit;
using AutoMapper;

using PixelDance.Shared.Infrastructure.Mapping;

namespace PixelDance.Tests.Modules.Recipes.Application.Fixtures
{
    public class MappingFixture : IAsyncLifetime
    {
        public IMapper? Mapper { get; private set; }

        public Task InitializeAsync()
        {
            Mapper = CreateMappingProfiles()
                .CreateMapper();

            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        internal static MapperConfiguration CreateMappingProfiles()
            => new(mc => mc.AddProfile(new MappingProfile()));

    }
}
