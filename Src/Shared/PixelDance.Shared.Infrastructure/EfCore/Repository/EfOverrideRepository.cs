using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;

using PixelDance.Shared.Kernel.Interfaces;

namespace PixelDance.Shared.Infrastructure.EfCore.Repository
{
    public class EfOverrideRepository<TContext, TEntity> 
        : RepositoryBase<TEntity>,
        //IResultRepository<TEntity>,
        IReadRepositoryBase<TEntity>
            where TEntity : class, IAggregateRoot
            where TContext : DbContext
    {
        public EfOverrideRepository(TContext dbContext) 
            : base(dbContext)
        { }

        /// <inheritdoc/>
        public override async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return await base.AddAsync(entity, cancellationToken);
        }
        /// <inheritdoc/>
        public override async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(entity, cancellationToken);
        }
        /// <inheritdoc/>
        public override async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await base.DeleteAsync(entity, cancellationToken);
        }
        /// <inheritdoc/>
        public override async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await base.DeleteRangeAsync(entities, cancellationToken);
        }
        /// <inheritdoc/>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public override async Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
        {
            return await base.GetByIdAsync(id, cancellationToken);
        }
        /// <inheritdoc/>
        public override async Task<TEntity?> GetBySpecAsync<Spec>(Spec specification, CancellationToken cancellationToken = default) 
            //where Spec : ISpecification<T>, ISingleResultSpecification
        {
            return await base.GetBySpecAsync(specification, cancellationToken);
        }
        ///// <inheritdoc/>
        //public override async Task<TResult?> GetBySpecAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default)
        //{
        //    return await base.GetBySpecAsync(specification, cancellationToken);
        //}

        /// <inheritdoc/>
        public override async Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await base.ListAsync(cancellationToken);
        }
        /// <inheritdoc/>
        public override async Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await base.ListAsync(cancellationToken);
        }
        /// <inheritdoc/>
        public override async Task<List<TResult>> ListAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default)
        {
            return await base.ListAsync(specification, cancellationToken);
        }

        /// <inheritdoc/>
        public override async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await base.CountAsync(specification, cancellationToken);
        }

        /// <inheritdoc/>
        public override async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await base.CountAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public override async Task<bool> AnyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await base.AnyAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public override async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await base.AnyAsync(cancellationToken);
        }
    }

}
