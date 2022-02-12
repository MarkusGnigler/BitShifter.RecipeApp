#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using AutoMapper;

using PixelDance.Shared.Abstractions.Mapping;
using PixelDance.Modules.Identity.Domain.AppUsers;

namespace PixelDance.Modules.Identity.Core.ViewModel
{
    public class AppUserVm : IMapFrom<AppUser>
    {
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 8)]
        public string Password { get; set; }
        public IEnumerable<string>? Roles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AppUser, AppUserVm>()
                .ForMember(
                        tok => tok.Roles,
                        opt => opt.MapFrom(src => src.UserRoles.Select(x => x.Role.Name))
                    );
        }
    }
}
