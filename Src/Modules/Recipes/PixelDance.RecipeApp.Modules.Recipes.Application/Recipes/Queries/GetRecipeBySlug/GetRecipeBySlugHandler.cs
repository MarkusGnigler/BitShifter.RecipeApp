using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

using MediatR;
using AutoMapper;

using PixelDance.Shared.Infrastructure.Guards;
using PixelDance.Modules.Recipes.Domain.Entities;
using PixelDance.Modules.Recipes.Domain.Specifications;
using PixelDance.Modules.Recipes.Application.Recipes.Queries.GetRecipeDetail;
using PixelDance.Shared.Abstractions.EfCore.Repository;

[assembly: InternalsVisibleTo("PixelDance.Tests.Modules.Recipes.Application")]
namespace PixelDance.Modules.Recipes.Application.Recipes.Queries.GetRecipeBySlug
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
