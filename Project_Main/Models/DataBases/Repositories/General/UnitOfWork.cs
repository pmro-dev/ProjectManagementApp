using Microsoft.EntityFrameworkCore;

namespace Project_Main.Models.DataBases.Repositories.General
{
    public abstract class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
	{
        protected readonly TContext _context;
        private bool _disposed;

        protected UnitOfWork(TContext context)
        {
            _context = context;
        }

        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

		public Task BeginTransactionAsync()
		{
			return _context.Database.BeginTransactionAsync();
		}

		public Task CommitTransactionAsync()
		{
			return _context.Database.CommitTransactionAsync();
		}

		public Task RollbackTransactionAsync()
		{
			return _context.Database.RollbackTransactionAsync();
		}

		public async Task<IEnumerable<string>> GetPendingMigrationsAsync()
		{
			return await _context.Database.GetPendingMigrationsAsync();
		}

        public Task MigrateAsync()
        {
			return _context.Database.MigrateAsync();
		}

		public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposed = true;
            }
        }
	}
}
