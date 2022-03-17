using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using BitShifter.Shared.Kernel.Common;
using BitShifter.Modules.Recipes.Domain.Entities;
using BitShifter.Modules.Recipes.Domain.ValueObjects;

namespace BitShifter.Modules.Recipes.Infrastructure.Persistence.Configurations
{
    //https://github.com/wmeints/efcore-ddd-demo/blob/main/src/Infrastructure/Data/PieEntityTypeConfiguration.cs

    //https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities
    internal class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            //modelBuilder.Entity<Recipe>()
            //   .FindNavigation(nameof(Recipe.Ingredients))
            //   .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsOne(x => x.PersonName, a =>
            {
                a.Property(f => f.FirstName)
                    .HasMaxLength(300)
                    .HasColumnName("FirstName")
                    .HasDefaultValue("");
                a.Property(f => f.LastName)
                    .HasMaxLength(300)
                    .HasColumnName("LastName")
                    .HasDefaultValue("");
            });

            //builder.OwnsMany(i => i.Ingredients, a => a.HasKey($"{typeof(Ingredient).Name}Id"));
            builder.OwnsMany(i => i.Ingredients, Configure);
            //builder.OwnsMany(i => i.DomainEvents, Configure);

            //Category relation
            //builder.HasOne<Category>().WithMany().HasForeignKey(x => x.Category);
        }


        public static void Configure(OwnedNavigationBuilder<Recipe, Ingredient> builder)
        {
            builder.WithOwner().HasForeignKey($"{typeof(Recipe).Name}Id");
        }

        public static void Configure(OwnedNavigationBuilder<Recipe, DomainEvent> builder)
        {
            builder.WithOwner().HasForeignKey($"{typeof(Recipe).Name}Id");
        }
    }
}
