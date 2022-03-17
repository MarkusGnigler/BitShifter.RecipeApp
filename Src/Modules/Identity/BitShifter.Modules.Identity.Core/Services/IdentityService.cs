using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using BitShifter.Shared.ROP;
using BitShifter.Modules.Identity.Core.Contracts;
using BitShifter.Modules.Identity.Core.Tokenizer;
using BitShifter.Modules.Identity.Core.ViewModel;
using BitShifter.Modules.Identity.Domain.AppUsers;
using BitShifter.Modules.Identity.Domain.AppUsers.Enums;

namespace BitShifter.Modules.Identity.Core.Services
{

    internal class IdentityService : AbstractService, IIdentityService
    {
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public IdentityService(
                ILogger<IdentityService> logger,
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

        private async Task<Result<object, Exception>> CheckIfUserExists(AppUserVm registerVm)
        {
            string username = SanitizeUserName(registerVm.UserName);

            var assignedUser = await _userManager.Users
                .FirstOrDefaultAsync(x =>
                    x.UserName == username);

            return assignedUser is null
                ? new object().Succeeded<object, Exception>()
                : new Exception("Der Benutzername ist bereits vergeben, bitte versuchen sie einen anderen.")
                    .Failed<object, Exception>();
        }

        private async Task<Result<AppUser, string[]>> CreateUser(AppUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded
                ? user.Succeeded<AppUser, string[]>()
                : result.Errors.Select(x => x.Description).ToArray()
                    .Failed<AppUser, string[]>();
        }

        private async Task<Result<AppUser, string[]>> AddUserRole(AppUser user)
        {
            var roleResult = await _userManager.AddToRoleAsync(user, RoleType.User.ToString());

            return roleResult.Succeeded
                ? user.Succeeded<AppUser, string[]>()
                : roleResult.Errors.Select(x => x.Description).ToArray()
                    .Failed<AppUser, string[]>();
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
                ? user.Succeeded<AppUser, string[]>()
                : new[] { $"Kein Benutzer mit dem Namen \"{username}\" gefunden." }
                    .Failed<AppUser, string[]>();
        }

        private async Task<Result<AppUser, string[]>> SignIn(AppUser user, AppUserVm loginVm)
        {
            var result = await _signInManager
                .CheckPasswordSignInAsync(user, loginVm.Password, lockoutOnFailure: false);

            return result.Succeeded
                ? user.Succeeded<AppUser, string[]>()
                : new[] { result.IsNotAllowed ? "Zugriff nicht erlaubt" : "Passwort falsch" }
                    .Failed<AppUser, string[]>();
        }

        #endregion

        private string SanitizeUserName(string username)
            => username.ToLower();

    }
}
