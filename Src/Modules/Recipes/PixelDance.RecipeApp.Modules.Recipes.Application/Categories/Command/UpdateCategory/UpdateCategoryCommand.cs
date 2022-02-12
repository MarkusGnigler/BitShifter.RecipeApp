using System;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using MediatR;
using AutoMapper;
using PixelDance.Shared.Infrastructure.Guards;
using PixelDance.Modules.Recipes.Domain.Entities;
using PixelDance.Shared.Abstractions.EfCore.Repository;

[assembly:InternalsVisibleTo("PixelDance.Tests.Modules.Recipes.Application")]
namespace PixelDance.Modules.Recipes.Application.Categories.Command.UpdateCategory
{
    public class UpdateCategory : IRequest<CategoryDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    
    internal class UpdateCategoryCommand : IRequestHandler<UpdateCategory, CategoryDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Category> _repository;

        public UpdateCategoryCommand(IMapper mapper, IRepository<Category> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CategoryDto> Handle(UpdateCategory request, CancellationToken cancellationToken)
        {
            var entityToUpdate = await _repository.GetByIdAsync(request.Id);

            Guard.AssertNotFound(entityToUpdate, $"No category with id \"{request.Id}\" found.");

            entityToUpdate.Update(request.Name);

            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CategoryDto>(entityToUpdate);
        }
    }
}
