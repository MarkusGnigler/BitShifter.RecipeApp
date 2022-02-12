using System;
using PixelDance.Modules.Recipes.Domain.Entities;

namespace PixelDance.Modules.Recipes.Domain.Exceptions
{
    public class CategoryIsAlreadyInUseException : Exception
    {
        public CategoryIsAlreadyInUseException(Category category)
            : base($"Categorie \"{category.Name}\" ist noch in Verwendung.")
        { }
    }
}
