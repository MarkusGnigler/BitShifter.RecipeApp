#pragma warning disable CS8618
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using PixelDance.Shared.ROP;

namespace PixelDance.Modules.Identity.Domain.AppUsers
{
    public class AppUser : IdentityUser<Guid>
    {
        //public ICollection<AppRole> UserRoles { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }

        //Only for ef core
        private AppUser() { }

        private AppUser(string username, string password)
        {
            var parameters = new[] { username, password };

            var validators = new (Func<string, bool> condition, Func<string, string> errorHandler)[]
            {
                (
                    x => x is null,
                    x => throw new NullReferenceException(nameof(x))
                ),
                (
                    x => string.IsNullOrWhiteSpace(x),
                    x => throw new ArgumentOutOfRangeException($"\"{x}\" is empty!")
                ),
            };

            _ = parameters.Select(
                x => validators.First(y => y.condition(x)).errorHandler(x));

            UserName = username;
            PasswordHash = password;
        }

        public static Result<AppUser, Exception> Create(string username, string password)
        {
            try
            {
                return new AppUser(username, password).Succeeded<AppUser, Exception>();
            }
            catch (Exception ex)
            {
                return ex.Failed<AppUser, Exception>();
            }
        }
    }
}
