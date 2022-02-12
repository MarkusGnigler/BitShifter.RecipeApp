using Ardalis.Specification;
using PixelDance.Modules.Recipes.Domain.Entities;

namespace PixelDance.Modules.Recipes.Domain.Specifications
{
    public class RecipeByCategoryNameSpec : Specification<Recipe>
    {
        public RecipeByCategoryNameSpec(string categoryName, bool asNoTracking = true)
        {
            Query
                .Where(x => x.Category.Name == categoryName)
                .Include(x => x.Category);

            if (asNoTracking)
                Query.AsNoTracking();
        }
    }
}
