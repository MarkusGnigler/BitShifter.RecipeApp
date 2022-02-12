using System.Linq;
using Ardalis.Specification;
using PixelDance.Modules.Recipes.Domain.Entities;

namespace PixelDance.Modules.Recipes.Domain.Specifications
{
    public class RecipeWithImgageSpec : Specification<Recipe>
    {
        public RecipeWithImgageSpec(string img)
        {
            Query.Where(x => x.Img == img);
        }
    }
}
