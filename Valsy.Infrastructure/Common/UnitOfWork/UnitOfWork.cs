using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Valsy.Application.Common.Interfaces;

namespace Valsy.Infrastructure.Common.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;
        private IDbContextTransaction _currentTransaction;
        private bool stopRollBack = false;

        public UnitOfWork(DbContext context, IDomainEventsDispatcher domainEventsDispatcher)
        {
            _context = context;
            _domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task BeginAsync()
        {
            _currentTransaction ??= await _context.Database.BeginTransactionAsync();
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            await _domainEventsDispatcher.DispatchEventsAsync();
            int result = await _context.SaveChangesAsync(cancellationToken);
            await _currentTransaction.CommitAsync();
            return result;
        }

        public void Dispose()
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        public async Task RollbackAsync()
        {
            if (!stopRollBack)
                await _currentTransaction.RollbackAsync();
        }

        public async Task SaveCurrentChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void StopRollBack()
        {
            stopRollBack = true;
        }
    }
}
