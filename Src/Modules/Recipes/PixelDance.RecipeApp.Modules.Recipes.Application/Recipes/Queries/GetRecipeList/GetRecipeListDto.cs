using System.Collections.Generic;
using PixelDance.Shared.Kernel.Enums;

namespace PixelDance.Modules.Recipes.Application.Recipes.Queries.GetRecipeList
{
    public class GetRecipeListDto
    {
        public string Title { get; set; }
        public int Position { get; set; }

        public PriorityLevel Priority { get; set; }

        public IEnumerable<GetRecipeDto> Recipes { get; set; }

    }
}
