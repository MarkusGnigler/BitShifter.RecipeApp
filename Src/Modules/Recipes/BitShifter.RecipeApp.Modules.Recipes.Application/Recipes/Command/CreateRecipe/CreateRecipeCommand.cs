using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using MediatR;
using AutoMapper;

using BitShifter.Shared.Infrastructure.Guards;
using BitShifter.Modules.Recipes.Domain.Entities;
using BitShifter.Modules.Recipes.Domain.Exceptions;
using BitShifter.Modules.Recipes.Domain.Specifications;
using BitShifter.Shared.Abstractions.EfCore.Repository;
using BitShifter.Modules.Recipes.Application.Recipes.Queries.GetRecipeDetail;

[assembly: InternalsVisibleTo("BitShifter.Tests.Modules.Recipes.Application")]
namespace BitShifter.Modules.Recipes.Application.Recipes.Command.CreateRecipe
{
    public class CreateRecipeCommand : IRequest<GetRecipeDetailDto>
    {
        public Guid Id { get; set; }
        public string Img { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Preparation { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public ICollection<IngredientsDto> Ingredients { get; set; }
    }

    internal  class CreateRecipeHandler : IRequestHandler<CreateRecipeCommand, GetRecipeDetailDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Recipe> _repository;
        private readonly IRepository<Category> _categoryRepository;

        public CreateRecipeHandler(IMapper mapper, IRepository<Recipe> repository, IRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<GetRecipeDetailDto> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            await ThrowIfTitleExists(request, cancellationToken);
            await ThrowIfSlugExists(request, cancellationToken);
            
            var category = await _categoryRepository
                .GetByIdAsync(request.CategoryId);
            Guard.AssertNotFound(category, $"No category with id \"{request.CategoryId}\" found.");

            var entityToAdd = new Recipe(
                request.Slug, request.Title, 
                request.Img, request.Preparation, 
                request.Description, category);

            var ingredientsToCreate = request.Ingredients
                .Select(x => Ingredient.Create(x.Title, x.Quantity, x.Unit, x.Priority))
                .ToArray();

            if (ingredientsToCreate?.Any() ?? false)
                entityToAdd.UpdateIngredients(ingredientsToCreate);

            await _repository.AddAsync(entityToAdd, cancellationToken); 

            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GetRecipeDetailDto>(entityToAdd);
        }

        private async Task ThrowIfTitleExists(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            var byTitleSpec = new RecipeByTitleSpec(request.Title);
            bool recipeNameTaken = await _repository
                .AnyAsync(byTitleSpec, cancellationToken);
            if (recipeNameTaken)
                throw new RecipeTitleAlreadyInUseException(request.Title);
        }

        private async Task ThrowIfSlugExists(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            var byTitleSpec = new RecipeBySlugSpec(request.Slug);
            bool recipeSlugTaken = await _repository
                .AnyAsync(byTitleSpec, cancellationToken);
            if (recipeSlugTaken)
                throw new RecipeSlugAlreadyInUseException(request.Slug);
        }
    }
}
