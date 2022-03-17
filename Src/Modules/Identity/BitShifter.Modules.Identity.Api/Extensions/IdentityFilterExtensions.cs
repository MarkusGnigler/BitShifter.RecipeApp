using Microsoft.AspNetCore.Mvc;
using BitShifter.Modules.Identity.Core.Filters;

namespace BitShifter.Modules.Identity.Api.Extensions
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
