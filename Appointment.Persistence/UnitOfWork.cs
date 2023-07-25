using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Appointment.Application.Common.Interfaces.Persistence;
using Appointment.Persistence.DbContexts.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Appointment.Persistence
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly EfCoreDbContext _dbContext;

        public UnitOfWork(EfCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            return _dbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        }
    }
}