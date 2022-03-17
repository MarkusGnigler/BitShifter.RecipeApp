using System;
using BitShifter.Modules.Recipes.Domain.Entities;

namespace BitShifter.Modules.Recipes.Domain.Exceptions
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
