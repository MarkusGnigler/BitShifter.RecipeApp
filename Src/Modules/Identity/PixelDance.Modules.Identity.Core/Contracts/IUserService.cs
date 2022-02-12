using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PixelDance.Modules.Identity.Core.ViewModel;
using PixelDance.Shared.ROP;

namespace PixelDance.Modules.Identity.Core.Contracts
{
    public interface IUserService
    {
        Task<Result<AppUserVm, string[]>> GetById(Guid id);
        Task<Result<IEnumerable<AppUserVm>, string[]>> GetAll();
        Task<Result<AppUserVm, string[]>> UpdateRoles(AppUserVm updateUser);
        Task<Result<Guid, string[]>> Delete(Guid id);
    }
}