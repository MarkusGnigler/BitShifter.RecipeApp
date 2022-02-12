using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using PixelDance.Shared.ROP;
using PixelDance.Modules.Identity.Core.Contracts;
using PixelDance.Modules.Identity.Core.Tokenizer;
using PixelDance.Modules.Identity.Core.ViewModel;
using PixelDance.Modules.Identity.Domain.AppUsers;
using PixelDance.Modules.Identity.Domain.AppUsers.Enums;

namespace PixelDance.Modules.Identity.Core.Services
{

    internal class IdentityService : AbstractService, IIdentityService
    {
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public IdentityService(
                ILogger<AbstractService> logger,
                ITokenService tokenService,
                UserManager<AppUser> userManager,
                SignInManager<AppUser> signInManager
            ) : base(logger, userManager)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        #region [ Register ]

        public Task<Result<IdentityUserVm, string[]>> Register(AppUserVm registerVm)
            => CheckIfUserExists(registerVm)
                .BindAsync(_ => Task.Run(() => AppUser.Create(SanitizeUserName(registerVm.UserName), registerVm.Password)))
                .MapFailureAsync(ex => Task.Run(() => new string[] { ex.Message }))
                .BindAsync(user => CreateUser(user, registerVm.Password))
                .BindAsync(user => AddUserRole(user))

                .TeeAsync(user => _logger.LogInformation("User \"{userName}\" has registered", user.UserName))
                .TeeFailureAsync(failure => _logger.LogInformation("Register failure: {failure}", string.Concat(failure)))

                .MapAsync(async user => user.AsUserVm(
                    await _tokenService.CreateToken(user)));

        private async Task<Result<AppUser, Exception>> CheckIfUserExists(AppUserVm registerVm)
        {
            string username = SanitizeUserName(registerVm.UserName);

            var assignedUser = await _userManager.Users
                .FirstOrDefaultAsync(x => 
                    x.UserName == username);

            return assignedUser is null 
                ? Result<AppUser, Exception>.Succeeded(assignedUser!)
                : Result<AppUser, Exception>.Failed(
                    new Exception("Der Benutzername ist bereits vergeben, bitte versuchen sie einen anderen."));
        }

        private async Task<Result<AppUser, string[]>> CreateUser(AppUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded
                ? Result<AppUser, string[]>.Succeeded(user)
                : Result<AppUser, string[]>.Failed(
                    result.Errors.Select(x => x.Description).ToArray());
        }

        private async Task<Result<AppUser, string[]>> AddUserRole(AppUser user)
        {
            var roleResult = await _userManager.AddToRoleAsync(user, RoleType.User.ToString());

            return roleResult.Succeeded
                ? Result<AppUser, string[]>.Succeeded(user)
                : Result<AppUser, string[]>.Failed(
                    roleResult.Errors.Select(x => x.Description).ToArray());
        }

        #endregion

        #region [ Login ]

        public Task<Result<IdentityUserVm, string[]>> Login(AppUserVm loginVm)
            => LoadUser(SanitizeUserName(loginVm.UserName))
                .BindAsync(user => SignIn(user, loginVm))

                .TeeAsync(user => _logger.LogInformation("User \"{userName}\" has logged in", user.UserName))
                .TeeFailureAsync(failure => _logger.LogInformation("Login failure: {failure}", string.Concat(failure)))

                .MapAsync(async user => user.AsUserVm(
                    await _tokenService.CreateToken(user)));

        private async Task<Result<AppUser, string[]>> LoadUser(string username)
        {
            var user = await _userManager.Users
                .Include(r => r.UserRoles)
                    .ThenInclude(r => r.Role)
                .SingleOrDefaultAsync(x => x.UserName == username);

            return user is not null
                ? Result<AppUser, string[]>.Succeeded(user)
                : Result<AppUser, string[]>.Failed(new[] { $"Kein Benutzer mit dem Namen \"{username}\" gefunden." });
        }

        private async Task<Result<AppUser, string[]>> SignIn(AppUser user, AppUserVm loginVm)
        {
            var result = await _signInManager
                .CheckPasswordSignInAsync(user, loginVm.Password, lockoutOnFailure: false);

            return result.Succeeded
                ? Result<AppUser, string[]>.Succeeded(user)
                : Result<AppUser, string[]>.Failed(new[] { result.IsNotAllowed ? "Zugriff nicht erlaubt" : "Passwort falsch" });
        }

        #endregion

        private string SanitizeUserName(string username)
            => username.ToLower();

    }
}
