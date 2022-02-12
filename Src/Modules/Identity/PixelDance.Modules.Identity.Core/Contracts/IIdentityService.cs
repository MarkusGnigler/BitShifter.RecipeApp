using System.Threading.Tasks;
using PixelDance.Modules.Identity.Core.ViewModel;
using PixelDance.Shared.ROP;

namespace PixelDance.Modules.Identity.Core.Contracts
{
    public interface IIdentityService
    {
        Task<Result<IdentityUserVm, string[]>> Register(AppUserVm registerVm);
        Task<Result<IdentityUserVm, string[]>> Login(AppUserVm registerVm);
    }
}
