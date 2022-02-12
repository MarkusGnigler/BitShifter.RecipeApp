using System;
using System.Collections.Generic;
using PixelDance.Shared.Kernel.Common;
using PixelDance.Shared.Kernel.Enums;
using PixelDance.Shared.Infrastructure.Guards;

namespace PixelDance.Modules.Recipes.Domain.Entities
{
    public class Ingredient : ValueObject
    {
        public string Title { get; private set; }
        public double Quantity { get; private set; }
        public string Unit { get; private set; }

        public PriorityLevel Priority { get; private set; }

        //Only for entity
        private Ingredient() { }

        public static Ingredient Create(string title, double quantity, string unit, PriorityLevel priority = PriorityLevel.None) 
            => new(title, quantity, unit, priority);
        private Ingredient(string title, double quantity, string unit, PriorityLevel priority)
            : base()
        {
            Title = Guard.AssertNotNullAndNotEmpty(title, "Title is required");
            Quantity = Guard.Should(quantity, x => x < 1, "Quantity is required");
            Unit = Guard.AssertNotNullAndNotEmpty(unit, "Unit is required");
            Priority = priority;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Title;
            yield return Unit;
            yield return Priority;
        }
    }

    //public static class IngredientExtension
    //{
    //    public static void Configure<T>(this OwnedNavigationBuilder<T, Ingredient> builder)
    //        where T : class
    //    {
    //        builder.WithOwner().HasForeignKey($"{typeof(T).Name}Id");
    //    }
    //}
}
