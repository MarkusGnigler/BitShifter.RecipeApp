using System.Linq;
using Ardalis.Specification;
using PixelDance.Modules.Recipes.Domain.Entities;

namespace PixelDance.Modules.Recipes.Domain.Specifications
{
    public class RecipeListSpec : Specification<Recipe>, ISingleResultSpecification
    {
        public RecipeListSpec()
        {
            Query
                .Include(c => c.Category)
                //.Include(i => i.Ingredients) // ValueObjects are eager!
                .OrderBy(t => t.Title)
                .AsNoTracking();
        }
    }
}
