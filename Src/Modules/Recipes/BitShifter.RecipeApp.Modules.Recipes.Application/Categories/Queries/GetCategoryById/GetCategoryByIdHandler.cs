using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using MediatR;
using AutoMapper;
using BitShifter.Modules.Recipes.Domain.Entities;
using BitShifter.Shared.Abstractions.EfCore.Repository;
using BitShifter.Shared.Infrastructure.Guards;

[assembly: InternalsVisibleTo("BitShifter.Tests.Modules.Recipes.Application")]
namespace BitShifter.Modules.Recipes.Application.Categories.Queries.GetCategoryById
{
    internal class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Category> _repository;

        public GetCategoryByIdHandler(IMapper mapper, IRepository<Category> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id);

            Guard.AssertNotFound(category, $"Keine Kategorie mit der Id \"{request.Id}\" gefunden.");

            return _mapper.Map<CategoryDto>(category);
        }
    }
}
