using System.Linq;
using Ardalis.Specification;
using BitShifter.Modules.Recipes.Domain.Entities;

namespace BitShifter.Modules.Recipes.Domain.Specifications
{
    public class RecipeWithImgageSpec : Specification<Recipe>
    {
        public RecipeWithImgageSpec(string img)
        {
            Query.Where(x => x.Img == img);
        }
    }
}
