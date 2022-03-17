using System;
using System.Linq;
using Ardalis.Specification;
using BitShifter.Modules.Recipes.Domain.Entities;

namespace BitShifter.Modules.Recipes.Domain.Specifications
{
    public class RecipeById : Specification<Recipe>, ISingleResultSpecification
    {
        public RecipeById(Guid id, bool asNoTracking = true)
        {
            Query
                .Where(x => x.Id == id)
                .Include(c => c.Category);
                //.Include(i => i.Ingredients) // ValueObjects are eager!

            if (asNoTracking)
                Query.AsNoTracking();
        }
    }
}
