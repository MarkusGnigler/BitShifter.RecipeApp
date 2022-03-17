using System.Collections.Generic;

namespace BitShifter.Modules.Recipes.Application.Categories.Queries.GetCategoryList
{
    public class GetCategoryListDto
    {
        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}
