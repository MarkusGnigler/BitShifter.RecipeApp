using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BitShifter.Shared.ROP;
using BitShifter.Shared.Kernel.Endpoints;
using BitShifter.Modules.Identity.Core.ViewModel;
using BitShifter.Modules.Identity.Core.Contracts;

namespace BitShifter.Modules.Identity.Api.Endpoints
{
    public class IdentityController : BaseApiController
    {

        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<IdentityUserVm>> Register(AppUserVm registerVm)
        {
            var userRegisterPipeline = await _identityService.Register(registerVm);

            var userResult = userRegisterPipeline
                .Match(
                    s => Ok(s), 
                    f => (ActionResult)BadRequest(string.Join("\n", f)));
            
            return userResult;
        }

        [HttpPost("login")]
        public async Task<ActionResult<IdentityUserVm>> Login([FromBody] AppUserVm loginVm)
        {
            var userRegisterPipeline = await _identityService.Login(loginVm);

            var userResult = userRegisterPipeline
                .Match(
                    s => Ok(s),
                    f => (ActionResult)BadRequest(string.Join("\n", f)));

            return userResult;
        }

    }
}
