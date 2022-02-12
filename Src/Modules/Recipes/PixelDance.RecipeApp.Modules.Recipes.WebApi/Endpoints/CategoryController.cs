using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using PixelDance.Shared.Kernel.Endpoints;
using PixelDance.Modules.Recipes.Application.Categories;
using PixelDance.Modules.Recipes.Application.Categories.Command.CreateCategory;
using PixelDance.Modules.Recipes.Application.Categories.Command.DeleteCategory;
using PixelDance.Modules.Recipes.Application.Categories.Command.UpdateCategory;
using PixelDance.Modules.Recipes.Application.Categories.Queries.GetCategoryList;
using PixelDance.Modules.Recipes.Application.Categories.Queries.GetCategoryById;

namespace PixelDance.Modules.Recipes.Api.Endpoints
{
    public class CategoryController : BaseApiController
    {
        private const string NOT_FOUND = "No category found";
        private const string NOT_FOUNDED = "No categories found";
        private const string ADDED_FAILED = "Category can't add";

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories(
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetCategoryListQuery(), cancellationToken);

            return GetActionResult(
                result.Categories, NOT_FOUNDED);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(Guid id,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetCategoryByIdQuery() { Id = id }, cancellationToken);

            return GetActionResult(
                result, NOT_FOUND);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> CreateCategory(Guid id, CreateCategory command, 
            CancellationToken cancellationToken)
        {
            if (id != command.Id) return BadRequest("Differ id!");

            var result = await Mediator.Send(command, cancellationToken);

            return GetActionResult(
                result, ADDED_FAILED);
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult<CategoryDto>> UpdateRecipe(Guid id, UpdateCategory command, 
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
            return await Mediator.Send(new DeleteCategory { Id = id }, cancellationToken);
        }

    }
}
