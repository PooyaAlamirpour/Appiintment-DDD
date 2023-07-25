using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Charisma.Application.Common.Interfaces.Persistence;
using Charisma.Domain.GenericCore.Abstractions;
using Charisma.Persistence.DbContexts.EfCore;
using Microsoft.EntityFrameworkCore;

namespace Charisma.Persistence.Common.Abstractions
{
    internal class AsyncRepository<TEntity, TId> : IAsyncRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : notnull
    {
        protected readonly EfCoreDbContext _dbContext;

        public AsyncRepository(EfCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        public Task<TEntity?> FindByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id.Equals(id), cancellationToken);
        }

        public Task<List<TEntity>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<TEntity>().ToListAsync(cancellationToken);
        }
        
        public async Task<IQueryable<TEntity>?> GetWithFilterAsync(Expression<Func<TEntity, bool>>? expression)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();
            if (expression is null) return query;
            
            query = query.Where(expression);
            return query;

        }
    }
}