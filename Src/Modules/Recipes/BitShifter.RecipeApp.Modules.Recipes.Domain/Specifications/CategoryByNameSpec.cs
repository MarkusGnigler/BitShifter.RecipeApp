using System.Linq;
using Ardalis.Specification;
using BitShifter.Modules.Recipes.Domain.Entities;

namespace BitShifter.Modules.Recipes.Domain.Specifications
{
    public class CategoryByNameSpec : Specification<Category>, ISingleResultSpecification
    {
        public CategoryByNameSpec(string name, bool asNoTracking = true)
        {
            Query.Where(x => x.Name == name);

            if (asNoTracking)
                Query.AsNoTracking();
        }
    }
}
