using System;
using BitShifter.Modules.Recipes.Domain.Entities;

namespace BitShifter.Modules.Recipes.Domain.Exceptions
{
    public class CategoryIsAlreadyInUseException : Exception
    {
        public CategoryIsAlreadyInUseException(Category category)
            : base($"Categorie \"{category.Name}\" ist noch in Verwendung.")
        { }
    }
}
