using Microsoft.EntityFrameworkCore;
using PixelDance.Shared.Infrastructure.EfCore;
using PixelDance.Shared.Abstractions.Interfaces;
using PixelDance.Modules.Recipes.Domain.Entities;
using PixelDance.Modules.Recipes.Application.Common.Interfaces;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PixelDance.Tests.Modules.Recipes.Application")]
namespace PixelDance.Modules.Recipes.Infrastructure.Persistence
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
