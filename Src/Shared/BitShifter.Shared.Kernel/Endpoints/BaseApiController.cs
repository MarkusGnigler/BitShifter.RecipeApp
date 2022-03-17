using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BitShifter.Shared.Kernel.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
    //[ServiceFilter(typeof(LogUserActivity))]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult<T> GetActionResult<T>(T response, string errorMessage)
            => response is null ? NotFound(errorMessage) : Ok(response);
        protected IActionResult GetIActionResult(object response, string errorMessage)
            => response is null ? NotFound(errorMessage) : Ok(response);
    }
}
