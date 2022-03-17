using MediatR;
using BitShifter.Modules.Recipes.Application.Recipes.Queries.GetRecipeDetail;

namespace BitShifter.Modules.Recipes.Application.Recipes.Queries.GetRecipeBySlug
{
    public class GetRecipeBySlugQuery : IRequest<GetRecipeBySlugDto>
    {
        public string Slug { get; set; }
        public GetRecipeBySlugQuery(string slug)
        {
            Slug = slug;
        }
    }
}
