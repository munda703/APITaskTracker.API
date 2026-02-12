using APITaskTracker.Infrastructure.Interface;
using APITaskTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace APITaskTracker.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly TaskTrackerDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<UnitOfWork> _logger;

        private IDbContextTransaction? _transaction;

        public UnitOfWork(
            TaskTrackerDbContext context,
            IServiceProvider serviceProvider,
            ILogger<UnitOfWork> logger)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public IGenericRepository<TEntity> Repository<TEntity>()
            where TEntity : class
        {
            return _serviceProvider.GetRequiredService<IGenericRepository<TEntity>>();
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
            {
                _logger.LogInformation("Starting database transaction");
                _transaction = await _context.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();

                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                    _logger.LogInformation("Database transaction committed successfully");

                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error committing database transaction");
                await RollbackAsync();
                throw;
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                _logger.LogWarning("Rolling back database transaction");

                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public Task<int> SaveChangesAsync()
        {
            _logger.LogDebug("Saving changes without explicit transaction");
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
