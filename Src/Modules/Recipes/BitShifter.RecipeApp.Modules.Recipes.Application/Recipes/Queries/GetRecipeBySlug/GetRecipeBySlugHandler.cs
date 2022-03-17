using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

using MediatR;
using AutoMapper;

using BitShifter.Shared.Infrastructure.Guards;
using BitShifter.Modules.Recipes.Domain.Entities;
using BitShifter.Modules.Recipes.Domain.Specifications;
using BitShifter.Modules.Recipes.Application.Recipes.Queries.GetRecipeDetail;
using BitShifter.Shared.Abstractions.EfCore.Repository;

[assembly: InternalsVisibleTo("BitShifter.Tests.Modules.Recipes.Application")]
namespace BitShifter.Modules.Recipes.Application.Recipes.Queries.GetRecipeBySlug
{
    internal class GetRecipeBySlugHandler : IRequestHandler<GetRecipeBySlugQuery, GetRecipeBySlugDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Recipe> _repository;

        public GetRecipeBySlugHandler(IMapper mapper, IRepository<Recipe> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GetRecipeBySlugDto> Handle(GetRecipeBySlugQuery request, CancellationToken cancellationToken)
        {
            var spec = new RecipeBySlugSpec(request.Slug);
            var entity = await _repository.GetBySpecAsync(spec, cancellationToken);

            Guard.AssertNotFound(entity, $"Es wurde kein Rezept mit der URL \"{request.Slug}\" gefunden.");

            return _mapper.Map<GetRecipeBySlugDto>(entity);
        }
    }
}
