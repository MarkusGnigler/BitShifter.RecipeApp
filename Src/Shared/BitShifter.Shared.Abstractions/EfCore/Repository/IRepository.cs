using Ardalis.Specification;
using BitShifter.Shared.Kernel.Interfaces;
using BitShifter.Shared.ROP;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BitShifter.Shared.Abstractions.EfCore.Repository
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