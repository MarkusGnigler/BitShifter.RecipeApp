using Ardalis.Specification;
using BitShifter.Modules.Recipes.Domain.Entities;

namespace BitShifter.Modules.Recipes.Domain.Specifications
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
