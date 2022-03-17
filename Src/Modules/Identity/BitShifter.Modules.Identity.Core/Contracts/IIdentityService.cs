using System.Threading.Tasks;
using BitShifter.Modules.Identity.Core.ViewModel;
using BitShifter.Shared.ROP;

namespace BitShifter.Modules.Identity.Core.Contracts
{
    public interface IIdentityService
    {
        Task<Result<IdentityUserVm, string[]>> Register(AppUserVm registerVm);
        Task<Result<IdentityUserVm, string[]>> Login(AppUserVm registerVm);
    }
}
