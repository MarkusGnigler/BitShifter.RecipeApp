using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PixelDance.Modules.Identity.Core.Contracts;
using PixelDance.Modules.Identity.Core.ViewModel;
using PixelDance.Modules.Identity.Domain.AppUsers;
using PixelDance.Modules.Identity.Domain.AppUsers.Enums;
using PixelDance.Shared.Infrastructure.Utilities;

namespace PixelDance.Modules.Identity.Core.Persistence.Seeding
{
    internal static class UserSeed
    {
        public static async Task SeedUserRoles(IdentityDbContext context, IServiceProvider provider)
        {
            // Seed, if necessary
            if (await context.Roles.AnyAsync()) return;

            var roles = new[]
            {
                new AppRole(RoleType.Admin.ToString()),
                new AppRole(RoleType.User.ToString()),
            };

            var roleManager = provider.GetService<RoleManager<AppRole>>();

            if (roleManager is null) return;

            foreach (var role in roles)
                await roleManager.CreateAsync(role);
        }

        public static async Task SeedAdminUser(IdentityDbContext context, IServiceProvider provider)
        {
            // Seed, if necessary
            if (await context.Users.AnyAsync()) return;

            var identityServices = provider.GetService<IIdentityService>();
            var userManager = provider.GetService<UserManager<AppUser>>();

            AppUserVm userVm = new() { UserName = "Markus-Gnigler", Password = "Password" };

            if (identityServices is null) return;
            await identityServices.Register(userVm);

            string username = userVm.UserName.ToLower();
            var adminUser = await context.Users
                .FirstAsync(user => user.UserName == username);

            if (userManager is null) return;
            var roleResult = await userManager
                .AddToRoleAsync(adminUser, RoleType.Admin.ToString());
        }
    }
}