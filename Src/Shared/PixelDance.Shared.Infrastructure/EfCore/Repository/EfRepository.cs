using Microsoft.EntityFrameworkCore;

using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;

using PixelDance.Shared.Kernel.Interfaces;
using PixelDance.Shared.Abstractions.EfCore.Repository;

namespace PixelDance.Shared.Infrastructure.EfCore.Repository
{
    public class EfRepository<TContext, TEntity> 
        : RepositoryBase<TEntity>,
        IRepository<TEntity>,
        IReadRepositoryBase<TEntity>
            where TEntity : class, IAggregateRoot
            where TContext : DbContext
    {
        public EfRepository(TContext dbContext) 
            : base(dbContext)
        { }
    }

}
