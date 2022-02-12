using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PixelDance.Modules.Identity.Core.Contracts;
using PixelDance.Modules.Identity.Domain.AppUsers;

namespace PixelDance.Modules.Identity.Core.Services
{
    internal abstract class AbstractService
    {
        protected readonly ILogger<AbstractService> _logger;
        protected readonly UserManager<AppUser> _userManager;

        protected AbstractService(
            ILogger<AbstractService> logger,
            UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }
    }
}
