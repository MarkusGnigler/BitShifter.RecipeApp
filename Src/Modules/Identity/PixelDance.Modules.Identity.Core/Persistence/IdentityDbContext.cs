using System;
using System.Threading;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using PixelDance.Shared.Kernel.Common;
using PixelDance.Shared.Abstractions.Interfaces;
using PixelDance.Modules.Identity.Domain.AppUsers;
using PixelDance.Shared.Infrastructure.EfCore.Utilities;

namespace PixelDance.Modules.Identity.Core.Persistence
{
    internal class IdentityDbContext
        : IdentityDbContext<AppUser, AppRole, Guid,
            IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>,
            IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        private readonly IDateTime? _dateTime;

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        { }

        //public IdentityDbContext(DbContextOptions<IdentityDbContext> options, IDateTime dateTime)
        //    : base(options)
        //{
        //    _dateTime = dateTime;
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("identity");

            builder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime?.Now ?? DateTime.Now.SetKindUtc();
                        break;

                    case EntityState.Modified:
                        //entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime?.Now ?? DateTime.Now.SetKindUtc();
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            //await DispatchEvents();

            return result;
        }

    }
}
