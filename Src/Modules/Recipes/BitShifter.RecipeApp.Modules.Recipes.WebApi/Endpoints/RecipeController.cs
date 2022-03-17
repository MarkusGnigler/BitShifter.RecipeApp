using System;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BitShifter.Shared.Kernel.Endpoints;
using BitShifter.Modules.Recipes.Application.Recipes.Command.CreateRecipe;
using BitShifter.Modules.Recipes.Application.Recipes.Command.DeleteRecipe;
using BitShifter.Modules.Recipes.Application.Recipes.Command.UpdateRecipe;
using BitShifter.Modules.Recipes.Application.Recipes.Queries.GetRecipeList;
using BitShifter.Modules.Recipes.Application.Recipes.Queries.GetRecipeBySlug;
using BitShifter.Modules.Recipes.Application.Recipes.Queries.GetRecipeDetail;

namespace BitShifter.Modules.Recipes.Api.Endpoints
{
    public class RecipeController : BaseApiController
    {
        private const string NOT_FOUND = "No recipe found";
        private const string NOT_FOUNDED = "No recipes found";
        private const string ADDED_FAILED = "Recipe can't add";

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetRecipeDto>>> GetAllRecipes(
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetRecipeListQuery(), cancellationToken);

            return GetActionResult(
                result.Recipes, NOT_FOUNDED);
        }

        //[Route("id")]
        //[HttpGet("{id}", Name = "GetRecipe")]
        //public async Task<ActionResult<GetRecipeDetailDto>> GetRecipe(Guid id, CancellationToken cancellationToken)
        //{
        //    return GetActionResult(
        //        await Mediator.Send(new GetRecipeDetailQuery(id)),
        //            NOT_FOUND);
        //}

        [HttpGet("{slug}", Name = "GetRecipeBySlug")]
        public async Task<ActionResult<GetRecipeBySlugDto>> GetRecipeBySlug(string slug, 
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetRecipeBySlugQuery(slug), cancellationToken);

            return GetActionResult(
                result, NOT_FOUND);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<GetRecipeDetailDto>> CreateRecipe(Guid id, CreateRecipeCommand command, 
            CancellationToken cancellationToken)
        {
            if (id != command.Id) return BadRequest("Differ id!");

            var result = await Mediator.Send(command, cancellationToken);

            return GetActionResult(
                result, ADDED_FAILED);
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult<GetRecipeDetailDto>> UpdateRecipe(Guid id, UpdateRecipeCommand command, 
            CancellationToken cancellationToken)
        {
            if (id != command.Id) return BadRequest("Differ id ");

            return await Mediator.Send(command, cancellationToken);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Guid>> DeleteRecipe(Guid id, 
            CancellationToken cancellationToken)
        {
            return await Mediator.Send(new DeleteRecipeCommand { Id = id }, cancellationToken);
        }

    }
}
