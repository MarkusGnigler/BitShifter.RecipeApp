using System.Threading;
using System.Threading.Tasks;

using MediatR;
using AutoMapper;

using PixelDance.Shared.Infrastructure.Guards;
using PixelDance.Modules.Recipes.Domain.Entities;
using PixelDance.Modules.Recipes.Domain.Specifications;
using PixelDance.Shared.Abstractions.EfCore.Repository;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PixelDance.Tests.Modules.Recipes.Application")]
namespace PixelDance.Modules.Recipes.Application.Recipes.Queries.GetRecipeDetail
{
    internal class GetRecipeDetailHandler : IRequestHandler<GetRecipeDetailQuery, GetRecipeDetailDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Recipe> _repository;

        public GetRecipeDetailHandler(IMapper mapper, IRepository<Recipe> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GetRecipeDetailDto> Handle(GetRecipeDetailQuery request, CancellationToken cancellationToken)
        {
            var getById = new RecipeById(request.Id);
            var entity = await _repository.GetBySpecAsync(getById, cancellationToken);

            Guard.AssertNotFound(entity, $"Es wurde kein Rezept mit der Id \"{request.Id}\" gefunden.");

            return _mapper.Map<GetRecipeDetailDto>(entity);
        }
    }
}
