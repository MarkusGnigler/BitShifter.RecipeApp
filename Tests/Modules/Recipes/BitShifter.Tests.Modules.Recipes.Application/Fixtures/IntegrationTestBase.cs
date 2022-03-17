using System;
using Microsoft.EntityFrameworkCore;

using Xunit;
using AutoMapper;

using BitShifter.Modules.Recipes.Domain.Entities;
using BitShifter.Shared.Abstractions.EfCore.Repository;
using BitShifter.Modules.Recipes.Infrastructure.Persistence;
using BitShifter.Modules.Recipes.Application.Common.Interfaces;
using BitShifter.Shared.Infrastructure.EfCore.Repository;
using BitShifter.Shared.Infrastructure.Guards;

namespace BitShifter.Tests.Modules.Recipes.Application.Fixtures
{
    [Collection(nameof(CollectionFixture))]
    public abstract class IntegrationTestBase : IDisposable
    {
        protected readonly IMapper mapper;
        protected readonly IApplicationDbContext context;
        protected readonly IRepository<Recipe> recipeRepository;
        protected readonly IRepository<Category> categoryRepository;

        public IntegrationTestBase(InMemoryDbContextFixture contextFixture, MappingFixture mappingFixture)
        {
            mapper = Guard.AssertNotNull(mappingFixture.Mapper, nameof(mappingFixture.Mapper));

            new InMemoryDbContextFixture().InitializeAsync().Wait();

            context = Guard.AssertNotNull(contextFixture.Context, nameof(contextFixture.Context));

            recipeRepository = new EfRepository<ApplicationDbContext, Recipe>(
                (ApplicationDbContext)context);
            categoryRepository = new EfRepository<ApplicationDbContext, Category>(
                 (ApplicationDbContext)context);
        }

        public void Dispose()
        {
            (context as DbContext)?.Database.EnsureDeleted();
        }
    }
}
