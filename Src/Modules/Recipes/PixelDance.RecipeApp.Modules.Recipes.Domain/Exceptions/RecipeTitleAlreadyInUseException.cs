using System;
using PixelDance.Modules.Recipes.Domain.Entities;

namespace PixelDance.Modules.Recipes.Domain.Exceptions
{
    public class RecipeTitleAlreadyInUseException : Exception
    {
        public RecipeTitleAlreadyInUseException(string title)
            : base($"Die Titel \"{title}\" ist bereits vergeben.")
        { }

        public RecipeTitleAlreadyInUseException(Recipe recipe)
            : base($"Die Titel \"{recipe.Title}\" ist bereits vergeben.")
        { }
    }
}
