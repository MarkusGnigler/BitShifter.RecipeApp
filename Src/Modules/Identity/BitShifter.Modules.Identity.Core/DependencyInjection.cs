using Microsoft.Extensions.DependencyInjection;
using BitShifter.Modules.Identity.Core.Contracts;
using BitShifter.Modules.Identity.Core.Persistence;
using BitShifter.Modules.Identity.Core.Services;
using BitShifter.Modules.Identity.Core.Tokenizer;
using BitShifter.Shared.Infrastructure.EfCore;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using BitShifter.Modules.Identity.Domain.AppUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BitShifter.Modules.Identity.Domain.AppUsers.Enums;

[assembly: InternalsVisibleTo("BitShifter.Modules.Identity.Api")]
namespace BitShifter.Modules.Identity.Core
{
    internal static class DependencyInjection
    {
        public static void AddIdentityCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDbContext>();

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IUserService, UserService>();

            AddIdentity(services, configuration);
        }

        private static void AddIdentity(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<AppUser>(option =>
            {
                option.Password.RequiredLength = 8;
                option.Password.RequireDigit = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;

                //option.SignIn.RequireConfirmedEmail = true;
            })
                .AddRoles<AppRole>()
                .AddRoleManager<RoleManager<AppRole>>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddRoleValidator<RoleValidator<AppRole>>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddTokenizer(configuration);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequiredAdminRole", policy => policy.RequireRole(RoleType.Admin.ToString()));
                //options.AddPolicy("ModeratorPhotoRole", policy => policy.RequireRole("Moderator"));
            });
        }
    }
}
