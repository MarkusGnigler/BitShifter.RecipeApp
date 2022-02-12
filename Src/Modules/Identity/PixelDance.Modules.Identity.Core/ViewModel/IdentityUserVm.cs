#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
using System;
using System.Linq;
using System.Collections.Generic;

using AutoMapper;

using PixelDance.Shared.Abstractions.Mapping;
using PixelDance.Modules.Identity.Domain.AppUsers;

namespace PixelDance.Modules.Identity.Core.ViewModel
{
    public class IdentityUserVm : IMapFrom<AppUser>
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public IEnumerable<string>? Roles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AppUser, IdentityUserVm>()
                .ForMember(
                        tok => tok.Token,
                        opt => opt.MapFrom(src => Guid.Empty.ToString())
                    )
                .ForMember(
                        tok => tok.Roles,
                        opt => opt.MapFrom(src => src.UserRoles.Select(x => x.Role.Name))
                    );
        }
    }

    internal static class IdentityUserVmExtensions
    {
        public static IdentityUserVm AsUserVm(this AppUser user, string token)
            => new()
            {
                UserName = user.UserName,
                Token = token,
                Roles = user.UserRoles.Select(x => x.Role.Name)
            };
    }
}
