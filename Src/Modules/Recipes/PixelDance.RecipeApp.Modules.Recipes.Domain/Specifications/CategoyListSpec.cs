using Ardalis.Specification;
using PixelDance.Modules.Recipes.Domain.Entities;

namespace PixelDance.Modules.Recipes.Domain.Specifications
{
    public class CategoyListSpec : Specification<Category>
    {
        public CategoyListSpec()
        {
            Query
                .OrderBy(x => x.Name)
                .AsNoTracking();
        }
    }
}
