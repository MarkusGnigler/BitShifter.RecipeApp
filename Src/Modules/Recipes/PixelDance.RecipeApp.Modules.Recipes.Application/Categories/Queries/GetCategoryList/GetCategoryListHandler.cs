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
namespace PixelDance.Modules.Recipes.Application.Categories.Queries.GetCategoryList
{
    internal class GetCategoryListHandler : IRequestHandler<GetCategoryListQuery, GetCategoryListDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Category> _repository;

        public GetCategoryListHandler(IMapper mapper, IRepository<Category> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GetCategoryListDto> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var loadAllSpec = new CategoyListSpec();
            var categories = await _repository.ListAsync(loadAllSpec, cancellationToken);

            var vm = new GetCategoryListDto
            {
                Categories = _mapper.Map<IEnumerable<CategoryDto>>(categories)
            };

            return vm;
        }
    }
}
