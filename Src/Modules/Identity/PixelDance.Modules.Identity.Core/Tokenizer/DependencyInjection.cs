using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace PixelDance.Modules.Identity.Core.Tokenizer
{
    internal static class DependencyInjection
    {
        public static void AddTokenizer(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

#if !DEBUG
                    options.IncludeErrorDetails = false;
                    options.RequireHttpsMetadata = true; 
#else
                    options.IncludeErrorDetails = true;
                    options.RequireHttpsMetadata = false;
#endif
                });
        }
    }
}
