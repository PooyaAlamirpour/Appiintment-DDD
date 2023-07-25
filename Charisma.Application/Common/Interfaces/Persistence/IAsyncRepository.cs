using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Charisma.Domain.GenericCore.Abstractions;

namespace Charisma.Application.Common.Interfaces.Persistence
{
    public interface IAsyncRepository<TEntity, in TId> where TEntity : Entity<TId> where TId : notnull
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        void Update(TEntity entity);
    
        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        Task<TEntity?> FindByIdAsync(TId id, CancellationToken cancellationToken = default);

        Task<List<TEntity>> FindAllAsync(CancellationToken cancellationToken = default);

        Task<IQueryable<TEntity>?> GetWithFilterAsync(Expression<Func<TEntity, bool>>? expression);
    }
}