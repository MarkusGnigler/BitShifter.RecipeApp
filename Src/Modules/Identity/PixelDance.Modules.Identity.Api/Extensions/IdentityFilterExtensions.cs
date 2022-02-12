using Microsoft.AspNetCore.Mvc;
using PixelDance.Modules.Identity.Core.Filters;

namespace PixelDance.Modules.Identity.Api.Extensions
{
    public static class IdentityFilterExtensions
    {
        public static MvcOptions AddIdentityFilters(this MvcOptions options)
        {
            options.Filters.Add<IdentityApiFilter>();

            return options;
        }
    }
}
