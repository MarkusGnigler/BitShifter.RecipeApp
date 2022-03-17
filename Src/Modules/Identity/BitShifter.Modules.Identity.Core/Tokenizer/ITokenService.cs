using System.Threading.Tasks;
using BitShifter.Modules.Identity.Domain.AppUsers;

namespace BitShifter.Modules.Identity.Core.Tokenizer
{
    internal interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
