using System;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

using MediatR;

using PixelDance.Shared.Infrastructure.Guards;
using PixelDance.Shared.Abstractions.Interfaces;
using PixelDance.Modules.Recipes.Domain.Entities;
using PixelDance.Modules.Recipes.Domain.Specifications;
using PixelDance.Shared.Abstractions.EfCore.Repository;

[assembly: InternalsVisibleTo("PixelDance.Tests.Modules.Recipes.Application")]
namespace PixelDance.Modules.Recipes.Application.Recipes.Command.DeleteRecipe
{
    public class DeleteRecipeCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    internal class DeleteRecipeHandler : IRequestHandler<DeleteRecipeCommand, Guid>
    {
        private readonly IWebRootWatcher _webRootWatcher;
        private readonly IRepository<Recipe> _repository;

        public DeleteRecipeHandler(IRepository<Recipe> repository, IWebRootWatcher webRootWatcher)
        {
            _repository = repository;
            _webRootWatcher = webRootWatcher;
        }

        public async Task<Guid> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
        {
            var getById = new RecipeById(request.Id, asNoTracking: false);
            var entityToDelete = await _repository.GetBySpecAsync(getById, cancellationToken);

            Guard.AssertNotFound(entityToDelete, $"No recipe with id \"{request.Id}\" found.");

            await _repository.DeleteAsync(entityToDelete, cancellationToken);
            
            await _repository.SaveChangesAsync(cancellationToken);

            var withImageSpec = new RecipeWithImgageSpec(entityToDelete.Img);
            bool isImageInUse = await _repository.AnyAsync(withImageSpec);

            if (!isImageInUse)
                await _webRootWatcher.RemoveFileAsync(entityToDelete.Img);

            return entityToDelete.Id;
        }
    }
}
