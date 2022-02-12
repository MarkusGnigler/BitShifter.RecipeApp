#pragma warning disable CS8618
using System;
using Microsoft.AspNetCore.Identity;

namespace PixelDance.Modules.Identity.Domain.AppUsers
{
    public class AppUserRole : IdentityUserRole<Guid>
    {
        public AppUser User { get; set; }
        public AppRole Role { get; set; }
    }
}
