using Microsoft.EntityFrameworkCore;
using BitShifter.Shared.Infrastructure.EfCore;
using BitShifter.Shared.Abstractions.Interfaces;
using BitShifter.Modules.Recipes.Domain.Entities;
using BitShifter.Modules.Recipes.Application.Common.Interfaces;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BitShifter.Tests.Modules.Recipes.Application")]
namespace BitShifter.Modules.Recipes.Infrastructure.Persistence
{
    internal class ApplicationDbContext : EfCoreContext<ApplicationDbContext>, IApplicationDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTime dateTime)
        //    : base(options, dateTime)
        //{ }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("recipes");

            builder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly());
        }

    }
}
