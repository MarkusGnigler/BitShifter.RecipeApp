using System;
using MediatR;

namespace BitShifter.Modules.Recipes.Application.Recipes.Queries.GetRecipeDetail
{
    public class GetRecipeDetailQuery : IRequest<GetRecipeDetailDto>
    {
        public Guid Id { get; set; }

        public GetRecipeDetailQuery(Guid id)
        {
            Id = id;
        }
    }
}
