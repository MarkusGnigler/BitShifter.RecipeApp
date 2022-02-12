using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using MediatR;
using AutoMapper;

using PixelDance.Shared.Kernel.Enums;
using PixelDance.Shared.Infrastructure.Guards;
using PixelDance.Modules.Recipes.Domain.Entities;
using PixelDance.Modules.Recipes.Domain.Specifications;
using PixelDance.Shared.Abstractions.EfCore.Repository;
using PixelDance.Modules.Recipes.Application.Recipes.Queries.GetRecipeDetail;

[assembly: InternalsVisibleTo("PixelDance.Tests.Modules.Recipes.Application")]
namespace PixelDance.Modules.Recipes.Application.Recipes.Command.UpdateRecipe
{
    public class UpdateRecipeCommand : IRequest<GetRecipeDetailDto>
    {
        public Guid Id { get; set; }
        public string Img { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Preparation { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public bool Liked { get; set; }
        public int Position { get; set; }
        public int Priority { get; set; }

        public ICollection<IngredientsDto> Ingredients { get; set; }

    }

    internal class UpdateRecipeHandler : IRequestHandler<UpdateRecipeCommand, GetRecipeDetailDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Recipe> _repository;
        private readonly IRepository<Category> _categoryRepository;

        public UpdateRecipeHandler(IMapper mapper, IRepository<Recipe> repository, IRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<GetRecipeDetailDto> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            var getById = new RecipeById(request.Id, asNoTracking: false);
            var entityToUpdate = await _repository.GetBySpecAsync(getById, cancellationToken);

            Guard.AssertNotFound(entityToUpdate, $"No recipe with id \"{request.Id}\" found.");

            Category category = null;
            if (entityToUpdate.Category.Id != request.CategoryId)
            {
                category = await _categoryRepository.GetByIdAsync(request.CategoryId);

                Guard.AssertNotFound(category, $"No category with id \"{request.CategoryId}\" found.");
            }

            entityToUpdate.Update(
                request.Slug, request.Title, request.Img,
                request.Preparation, request.Description,
                request.Liked, request.Position, category ?? entityToUpdate.Category,
                (PriorityLevel)request.Priority);

            var ingredientsToUpdate = request.Ingredients
                .Select(x => Ingredient.Create(x.Title, x.Quantity, x.Unit, x.Priority))
                .ToArray();

            if (ingredientsToUpdate?.Any() ?? false)
                entityToUpdate.UpdateIngredients(ingredientsToUpdate);

            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GetRecipeDetailDto>(entityToUpdate);
        }
    }
}
