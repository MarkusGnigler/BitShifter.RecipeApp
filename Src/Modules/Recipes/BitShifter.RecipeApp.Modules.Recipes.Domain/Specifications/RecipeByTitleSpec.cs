using System.Linq;
using Ardalis.Specification;
using BitShifter.Modules.Recipes.Domain.Entities;

namespace BitShifter.Modules.Recipes.Domain.Specifications
{
    public class RecipeByTitleSpec : Specification<Recipe>
    {
        public RecipeByTitleSpec(string title, bool asNoTracking = true)
        {
            Query.Where(x => x.Title == title);

            if (asNoTracking)
                Query.AsNoTracking();
        }
    }
}
