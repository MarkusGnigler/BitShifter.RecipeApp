using Microsoft.Extensions.DependencyInjection;
using PixelDance.Modules.Identity.Core.Contracts;
using PixelDance.Modules.Identity.Core.Persistence;
using PixelDance.Modules.Identity.Core.Services;
using PixelDance.Modules.Identity.Core.Tokenizer;
using PixelDance.Shared.Infrastructure.EfCore;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PixelDance.Modules.Identity.Domain.AppUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

[assembly: InternalsVisibleTo("PixelDance.Modules.Identity.Api")]
namespace PixelDance.Modules.Identity.Core
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
                options.AddPolicy("RequiredAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("ModeratorPhotoRole", policy => policy.RequireRole("Moderator"));
            });
        }
    }
}
