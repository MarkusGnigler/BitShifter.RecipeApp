using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using PixelDance.Shared.Kernel.Endpoints;
using PixelDance.Modules.Identity.Api.Extensions;

namespace PixelDance.Modules.Identity.Api.Endpoints
{
    //[Authorize]
    public class ClaimsTesterController : BaseApiController
    {

        [HttpGet]
        public IActionResult GetClaims()
        {
            var userId = User.GetUserId();
            var userName = User.GetUserName();
            var userRoles = User.GetUserRoles();

            var result = new 
            { 
                Id = userId.ToString(), 
                Name = userName,
                Roles = userRoles
            };

            return Ok(result);
        }

        [Authorize]
        [HttpGet("authorize")]
        public IActionResult CheckAuthorizeAttribute()
        {
            return Ok("Authorized!");
        }
    }
}
