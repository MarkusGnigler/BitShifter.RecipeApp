using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using MediatR;
using AutoMapper;

using PixelDance.Modules.Recipes.Domain.Entities;
using PixelDance.Modules.Recipes.Domain.Specifications;
using PixelDance.Shared.Abstractions.EfCore.Repository;

[assembly: InternalsVisibleTo("PixelDance.Tests.Modules.Recipes.Application")]
namespace PixelDance.Modules.Recipes.Application.Recipes.Queries.GetRecipeList
{
    internal class GetRecipeListHandler : IRequestHandler<GetRecipeListQuery, GetRecipeListDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Recipe> _repository;

        public GetRecipeListHandler(IMapper mapper, IRepository<Recipe> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GetRecipeListDto> Handle(GetRecipeListQuery request, CancellationToken cancellationToken)
        {
            var loadAllRecipesSpec = new RecipeListSpec();
            var recipes = await _repository.ListAsync(loadAllRecipesSpec, cancellationToken);

            //var priorityLevels = Enum.GetValues(typeof(PriorityLevel))
            //    .Cast<PriorityLevel>()
            //    .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
            //    .ToList();

            var vm = new GetRecipeListDto
            {
                Recipes =  _mapper.Map<IEnumerable<GetRecipeDto>>(recipes)
            };

            return vm;
        }
    }
}
