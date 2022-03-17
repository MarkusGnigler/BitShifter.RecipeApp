using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using BitShifter.Shared.Kernel.Common;
using BitShifter.Modules.Recipes.Domain.Entities;

namespace BitShifter.Modules.Recipes.Infrastructure.Persistence.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(250);

            //builder.OwnsMany(i => i.DomainEvents, Configure);
        }

        public static void Configure(OwnedNavigationBuilder<Category, DomainEvent> builder)
        {
            builder.WithOwner().HasForeignKey($"{typeof(Category).Name}Id");
        }
    }
}
