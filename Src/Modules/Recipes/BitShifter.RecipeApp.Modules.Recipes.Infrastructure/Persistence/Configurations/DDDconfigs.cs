using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BitShifter.Modules.Recipes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitShifter.Modules.Recipes.Infrastructure.Persistence.Configurations
{
    internal class DDDconfigs : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> modelBuilder)
        {
            //Key property is private´, configure EF Core to find it
            //private string _id;
            //private string Id => _id;
            //modelBuilder.Entity<Ingredient>()
            //    .HasKey("Id");

            //Specify backing field for undiscoverable properties - Propertie (TeamName) has no getter or setter
            //private string _quantity;
            //public string Quantity => _quantity;
            //modelBuilder.Entity<Ingredient>()
            //    .Property(b => b.Quantity)
            //    .HasField("_quantity");

            //Value objects ...map as owned entities of the types that use them as properties
            //There is also OwnedMany support
            //modelBuilder.Entity<Recipe>().OwnsOne(x => x.Id).Property(y = y.PersonName).HasOne();
            //modelBuilder.Entity<Recipe>().OwnsOne(x => x.Id);
            //modelBuilder.Entity<Recipe>().OwnsOne(x => x.Id, hc =>
            //{
            //    hc.Property(u => u.Primary).HasConversion(c => c.Name, s => ConsoleColor.FromName(s));
            //    hc.Property(u => u.Secondary).HasConversion(c => c.Name, s => ConsoleColor.FromName(s));
            //});

            //Shadow properties
            //modelBuilder.Entity<Recipe>().Property<DateTime>("Created");
            //modelBuilder.Entity<Recipe>().Property<DateTime>("LastModified");

            //https://nodogmablog.bryanhogan.net/2018/09/saving-enums-with-entity-framework-core/
            //builder.Property(e => e.Title)
            //    .HasConversion(
            //        x => x.ToString(), // to converter
            //        x => (DeliveryPreference)Enum.Parse(typeof(DeliveryPreference), x));// from converter



            //Use a backing field for the id property
            //builder.Property(x => x.Id)
            //    .HasField("_id")
            //    .UsePropertyAccessMode(PropertyAccessMode.Field);


            //builder.Property(x => x.Description).HasMaxLength(2500);
            //builder.OwnsOne(x => x.Preparation);
            //builder.Property(x => x.Preparation).HasConversion(
            //    input => System.Text.Json.JsonSerializer.Serialize(input),
            //    output => System.Text.Json.JsonSerializer.Deserialize(output),
            //);

            // Mark the ingredients as an owned collection.
            //// And set a backing field for it.
            //var ingredientsConfiguration = builder.OwnsMany(x => x.Ingredients);

            //ingredientsConfiguration.WithOwner().HasForeignKey("RecipeId");
            //ingredientsConfiguration.Property<int>("Id");
            //ingredientsConfiguration.HasKey("Id");

            // You own configure the rules for owned entities using the returned configuration builder instance.
            //ingredientsConfiguration.Property(x => x.Title)
            //    .HasMaxLength(250)
            //    .IsRequired();

            //builder.Navigation(x => x.Ingredients).Metadata.SetField("_ingredients");

            //var navigation = builder.Metadata.FindNavigation("Ingredients");
            //navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            //builder
            //   .Navigation(nameof(Recipe.Ingredients))
            //   .Metadata.SetField(PropertyAccessMode.Field);

        }
    }
}
