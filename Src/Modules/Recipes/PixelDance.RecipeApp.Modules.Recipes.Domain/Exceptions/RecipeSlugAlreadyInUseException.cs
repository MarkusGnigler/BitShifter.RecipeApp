using System;
using PixelDance.Modules.Recipes.Domain.Entities;

namespace PixelDance.Modules.Recipes.Domain.Exceptions
{
    public class RecipeSlugAlreadyInUseException : Exception
    {
        public RecipeSlugAlreadyInUseException(string slug)
            : base($"Die Url \"{slug}\" ist bereits vergeben.")
        { }

        public RecipeSlugAlreadyInUseException(Recipe recipe)
            : base($"Die Url \"{recipe.Slug}\" ist bereits vergeben.")
        { }
    }
}
