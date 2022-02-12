using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using PixelDance.Shared.Kernel.Common;
using PixelDance.Shared.Abstractions.Interfaces;
using PixelDance.Shared.Infrastructure.EfCore.Utilities;

namespace PixelDance.Shared.Infrastructure.EfCore
{
    public abstract class EfCoreContext<TContext> : DbContext 
        where TContext : DbContext
    {
        private readonly IDateTime _dateTime;

        public EfCoreContext(DbContextOptions<TContext> options)
            : base(options)
        { }

        public EfCoreContext(DbContextOptions<TContext> options, IDateTime dateTime)
            : base(options)
        {
            _dateTime = dateTime;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //UTC
            builder.ApplyUtcDateTimeConverter();
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

        //private async Task DispatchEvents()
        //{
        //    while (true)
        //    {
        //        var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
        //            .Select(x => x.Entity.DomainEvents)
        //            .SelectMany(x => x)
        //            .Where(domainEvent => !domainEvent.IsPublished)
        //            .FirstOrDefault();
        //        if (domainEventEntity == null) break;

        //        domainEventEntity.IsPublished = true;

        //        await _domainEventService.Publish(domainEventEntity);
        //    }
        //}
    }
}
