using System.Threading.Tasks;
using PixelDance.Modules.Identity.Domain.AppUsers;

namespace PixelDance.Modules.Identity.Core.Tokenizer
{
    internal interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
