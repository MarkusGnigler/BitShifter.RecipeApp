using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using BitShifter.Shared.ROP;
using BitShifter.Modules.Identity.Core.Contracts;
using BitShifter.Modules.Identity.Core.ViewModel;
using BitShifter.Modules.Identity.Domain.AppUsers;
using AutoMapper.QueryableExtensions;

namespace BitShifter.Modules.Identity.Core.Services
{
    internal class UserService : AbstractService, IUserService
    {
        private readonly IMapper _mapper;

        public UserService(
            IMapper mapper,
            ILogger<AbstractService> logger, 
            UserManager<AppUser> userManager) 
                : base(logger, userManager)
        {
            _mapper = mapper;
        }

        #region [ ReadSide ]

        public async Task<Result<AppUserVm, string[]>> GetById(Guid id)
        {
            var user = await _userManager.Users
                .Include(r => r.UserRoles)
                    .ThenInclude(r => r.Role)
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            var result = user is not null
                ? user.Succeeded<AppUser, string[]>()
                : new[] { $"Kein Benutzer mit der ID \"{id}\" gefunden." }
                    .Failed<AppUser, string[]>();

            return result
                .Map(user => _mapper.Map<AppUserVm>(user))
                .Tee(user => _logger.LogInformation("User \"{username}\" was found.", user.UserName))
                .TeeFailure(error => _logger.LogInformation("Error: {error}", error));
        }

        public async Task<Result<IEnumerable<AppUserVm>, string[]>> GetAll()
        {
            var users = await _userManager.Users
                .Include(r => r.UserRoles)
                    .ThenInclude(r => r.Role)
                .AsNoTracking()
                .ToListAsync();

            var result = users is not null
                ? users.Succeeded<IEnumerable<AppUser>, string[]>()
                : new[] { $"Keine Benutzer gefunden." }
                    .Failed<IEnumerable<AppUser>, string[]>();

            return result
                .Map(users => users.Select(x => _mapper.Map<AppUserVm>(x)))
                .Tee(users => _logger.LogInformation("Users \"{usernames}\" was found.", string.Join(",", users.Select(x => x.UserName))))
                .TeeFailure(error => _logger.LogInformation("Error: {error}", error));
        }

        #endregion

        #region [ WriteSide ]

        public Task<Result<AppUserVm, string[]>> UpdatePassword(AppUserVm updateUserVm)
            => LoadUser(updateUserVm.Id)
                .BindAsync(async user =>
                {
                    var identityResult = await _userManager.ChangePasswordAsync(user, "", updateUserVm.Password);

                    return ParseIdentity(user, identityResult);
                })
                .MapAsync(user => _mapper.Map<AppUserVm>(user))

                .TeeAsync(user => _logger.LogInformation($"User password \"{user.UserName}\" was updated."))
                .TeeFailureAsync(error => _logger.LogInformation("Error: {error}", error));

        public Task<Result<AppUserVm, string[]>> UpdateRoles(AppUserVm updateUser)
            => LoadUser(updateUser.Id)
                .BindAsync(async user =>
                {
                    updateUser.Roles = updateUser.Roles ?? Array.Empty<string>();

                    var rolesToDelete = user.UserRoles
                        .Select(x => x.Role.Name)
                        .Where(x => !updateUser.Roles.Contains(x));

                    var identityResult = await _userManager.RemoveFromRolesAsync(user, rolesToDelete);

                    return ParseIdentity(user, identityResult);
                })
                .BindAsync(async user =>
                {
                    updateUser.Roles = updateUser.Roles ?? Array.Empty<string>();

                    var roleNames = user.UserRoles.Select(x => x.Role.Name);
                    var rolesToAdd = updateUser.Roles
                        .Where(x => !roleNames.Contains(x));

                    var identityResult = await _userManager.AddToRolesAsync(user, rolesToAdd);

                    return ParseIdentity(user, identityResult);
                })
                .MapAsync(user => _mapper.Map<AppUserVm>(user))

                .TeeAsync(user => _logger.LogInformation($"User roles \"{user.UserName}\" was updated."))
                .TeeFailureAsync(error => _logger.LogInformation("Error: {error}", error));

        public Task<Result<Guid, string[]>> Delete(Guid id)
            => LoadUser(id)
                .BindAsync(async user => ParseIdentity(user, await _userManager.DeleteAsync(user)))
                .MapAsync(user => user.Id)

                .TeeAsync(guid => _logger.LogInformation($"User \"{guid}\" was deleted."))
                .TeeFailureAsync(error => _logger.LogInformation("Error: {error}", error));

        #endregion

        #region [ Private methods ]

        private async Task<Result<AppUser, string[]>> LoadUser(Guid id)
        {
            var user = await _userManager.Users
                .Include(c => c.UserRoles)
                    .ThenInclude(x => x.Role)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return user is not null
                ? user.Succeeded<AppUser, string[]>()
                : new[] { $"Kein Benutzer mit der ID \"{id}\" gefunden." }
                    .Failed<AppUser, string[]>();
        }

        private Result<AppUser, string[]> ParseIdentity(AppUser user, IdentityResult result)
            => result.Succeeded
                ? user.Succeeded<AppUser, string[]>()
                : result.Errors.Select(x => x.Description).ToArray()
                    .Failed<AppUser, string[]>();

        #endregion

    }
}
