using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BitShifter.Shared.ROP;
using BitShifter.Shared.Kernel.Endpoints;
using BitShifter.Modules.Identity.Core.ViewModel;
using BitShifter.Modules.Identity.Core.Contracts;

namespace BitShifter.Modules.Identity.Api.Endpoints
{
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUserVm>>> GetAllUsers()
        {
            var userPipeline = await _userService.GetAll();

            return userPipeline.Match(
                s => Ok(s),
                f => (ActionResult)BadRequest(string.Join("\n", f)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IdentityUserVm>> GetUserById(Guid id)
        {
            var userPipeline = await _userService.GetById(id);

            return userPipeline.Match(
                s => Ok(s),
                f => (ActionResult)BadRequest(string.Join("\n", f)));
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<IdentityUserVm>> UpdateUserRoles(Guid id, AppUserVm updateUserVm)
        {
            if (id != updateUserVm.Id) return BadRequest("Id differ!");

            var userPipeline = await _userService.UpdateRoles(updateUserVm);

            return userPipeline.Match(
                s => Ok(s),
                f => (ActionResult)BadRequest(string.Join("\n", f)));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteUser(Guid id)
        {
            var userPipeline = await _userService.Delete(id);

            return userPipeline.Match(
                s => Ok(s),
                f => (ActionResult)BadRequest(string.Join("\n", f)));
        }

    }
}
