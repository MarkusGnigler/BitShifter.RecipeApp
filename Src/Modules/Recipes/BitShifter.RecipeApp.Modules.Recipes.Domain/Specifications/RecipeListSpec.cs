using System.Linq;
using Ardalis.Specification;
using BitShifter.Modules.Recipes.Domain.Entities;

namespace BitShifter.Modules.Recipes.Domain.Specifications
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
