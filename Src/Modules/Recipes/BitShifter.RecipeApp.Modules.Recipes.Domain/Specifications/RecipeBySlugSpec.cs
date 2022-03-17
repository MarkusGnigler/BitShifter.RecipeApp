using Ardalis.Specification;
using BitShifter.Modules.Recipes.Domain.Entities;

namespace BitShifter.Modules.Recipes.Domain.Specifications
{
    public class RecipeBySlugSpec : Specification<Recipe>, ISingleResultSpecification
    {
        public RecipeBySlugSpec(string slug, bool asNoTracking = true)
        {
            Query
                .Where(x => x.Slug == slug)
                //.Include(i => i.Ingredients) // ValueObjects are eager!
                .Include(c => c.Category);

            if (asNoTracking)
                Query.AsNoTracking();
        }
    }
}
