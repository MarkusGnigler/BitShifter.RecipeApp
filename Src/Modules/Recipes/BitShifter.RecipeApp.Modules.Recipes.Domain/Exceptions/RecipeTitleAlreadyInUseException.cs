using System;
using BitShifter.Modules.Recipes.Domain.Entities;

namespace BitShifter.Modules.Recipes.Domain.Exceptions
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
