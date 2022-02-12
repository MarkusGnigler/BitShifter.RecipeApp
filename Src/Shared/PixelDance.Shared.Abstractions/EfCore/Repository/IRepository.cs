using Ardalis.Specification;
using PixelDance.Shared.Kernel.Interfaces;
using PixelDance.Shared.ROP;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PixelDance.Shared.Abstractions.EfCore.Repository
{

    /// <inheritdoc/>
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {
    }

    /// <inheritdoc/>
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}