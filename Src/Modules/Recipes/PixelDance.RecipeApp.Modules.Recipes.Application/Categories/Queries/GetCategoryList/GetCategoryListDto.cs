using System.Collections.Generic;

namespace PixelDance.Modules.Recipes.Application.Categories.Queries.GetCategoryList
{
    public class GetCategoryListDto
    {
        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}
