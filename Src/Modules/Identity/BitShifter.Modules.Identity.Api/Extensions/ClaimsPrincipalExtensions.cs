using System;
using System.Linq;
using System.Security.Claims;

namespace BitShifter.Modules.Identity.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
            => Guid.Parse(
                principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());

        public static string GetUserName(this ClaimsPrincipal principal)
            => principal.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;

        public static string[] GetUserRoles(this ClaimsPrincipal principal)
            => principal
                .FindAll(ClaimTypes.Role)
                .Select(x => x.Value)
                .ToArray() ?? Array.Empty<string>();

    }
}
