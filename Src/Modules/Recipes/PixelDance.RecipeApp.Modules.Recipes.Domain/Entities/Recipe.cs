using System;
using System.Linq;
using System.Collections.Generic;
using PixelDance.Shared.Kernel.Enums;
using PixelDance.Shared.Kernel.Common;
using PixelDance.Shared.Kernel.Interfaces;
using PixelDance.Shared.Kernel.ValueObjects;
using PixelDance.Shared.Infrastructure.Guards;
using PixelDance.Modules.Recipes.Domain.Events;

namespace PixelDance.Modules.Recipes.Domain.Entities
{
    public class Recipe : AuditableEntity, IAggregateRoot//, IHasDomainEvent
    {
        public PersonFullName PersonName { get; set; }

        public string Img { get; private set; }
        public string Slug { get; private set; }
        public string Title { get; private set; }
        public string Preparation { get; private set; }
        public string Description { get; private set; }
        public bool Liked { get; private set; }
        public int Position { get; private set; }

        public Category Category { get; private set; }

        private List<Ingredient> _ingredients = new();
        public IEnumerable<Ingredient> Ingredients => _ingredients.ToList();

        //public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();

        //Only for ef
        private Recipe() { }

        public Recipe(
            string slug, string title, string img, string preparation, string description, 
            Category category, PriorityLevel priority = PriorityLevel.None)
                : base()
        {
            Update(slug, title, img, preparation, description, false, 1, category, priority);

            //DomainEvents.Add(new RecipeCreated(Id));
        }

        public void Update(
            string slug, string title, string img, string preparation, string description, 
            bool liked, int position, Category category, PriorityLevel priority = PriorityLevel.None)
        {
            Guard.AssertNotNullAndNotEmpty<InvalidOperationException>(
                new[] { slug, title, img, preparation, description });

            Slug = slug;
            Img = img;
            Title = title;
            Category = category;
            Preparation = preparation;
            Description = description;
            Liked = liked;
            Position = Guard.Should(position, x => x < 1, "Position must be greater then 0");

            if (priority != PriorityLevel.None)
                Priority = priority;
        }

        public void UpdateIngredients(IEnumerable<Ingredient> newIngredients)
        {
            Guard.AssertNotNull(newIngredients, nameof(newIngredients));
            Guard.Assert(newIngredients, $"Must specify at least one ingredient {nameof(newIngredients)}", 
                x => !x.Any());
            Guard.Assert(newIngredients, "The relative amount of all ingredients combined must add up to 1.0", 
                x => x.Sum(x => x.Quantity) < 1);

            _ingredients = newIngredients.ToList();
        }

        public void UpdateIngredients(Ingredient ingredient, Ingredient newIngredient)
        {
            Guard.AssertNotNull(ingredient, nameof(ingredient));
            Guard.AssertNotNull(newIngredient, nameof(newIngredient));
            Guard.Assert(newIngredient, "The relative amount of all ingredients combined must add up to 1.0",
                x => x.Quantity < 1);

            _ingredients = _ingredients
                .Select(x => !x.Equals(ingredient) ? x : newIngredient)
                .ToList();
        }

        public void InsertIngredient(Ingredient ingredient, PriorityLevel priority = PriorityLevel.None)
        {
            Guard.AssertNotNull(ingredient, nameof(ingredient));

            _ingredients.Add(ingredient);
        }

        public void InsertIngredient(string title, double quantity, string unit, PriorityLevel priority = PriorityLevel.None)
        {
            Guard.AssertNotNull(title, nameof(title));
            Guard.AssertNotNull(unit, nameof(unit));
            if (quantity < 1) throw new ArgumentOutOfRangeException(nameof(quantity));

            _ingredients.Add(
                Ingredient.Create(title, quantity, unit, priority));
        }

        public void RemoveIngredient(string title, double quantity, string unit)
        {
            if (_ingredients is null) throw new ArgumentNullException(nameof(_ingredients));

            var ingredientToRemove = _ingredients.FirstOrDefault(
                x => x.Equals(Ingredient.Create(title, quantity, unit)));

            if (ingredientToRemove is not null)
                _ingredients.Remove(ingredientToRemove);
        }

    }
}
