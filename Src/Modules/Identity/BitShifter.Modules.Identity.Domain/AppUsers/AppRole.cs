#pragma warning disable CS8618
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BitShifter.Modules.Identity.Domain.AppUsers
{
    public class AppRole : IdentityRole<Guid>
    {
        //public ICollection<AppUser> UserRoles { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }

        //Only for ef core
        private AppRole() { }

        public AppRole(string roleName)
            : base(roleName)
        {
            Id = Guid.NewGuid();
        }
    }
}
