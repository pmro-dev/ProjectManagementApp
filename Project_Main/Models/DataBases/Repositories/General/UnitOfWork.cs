using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Project_Main.Models.DataBases.Repositories.General
{
    public abstract class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
	{
        protected readonly TContext _context;
        private bool _disposed = false;

        protected UnitOfWork(TContext context)
        {
            _context = context;
        }

        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

		public virtual void SaveChanges()
		{
			_context.SaveChanges();
		}

		public IDbContextTransaction BeginTransaction()
		{
			return _context.Database.BeginTransaction();
		}

		public void CommitTransaction()
		{
            _context.Database.CommitTransaction();
		}

		public void RollbackTransaction()
		{
			_context.Database.RollbackTransaction();
		}

		public IEnumerable<string> GetPendingMigrations()
		{
			return _context.Database.GetPendingMigrations();
		}

        public void Migrate()
        {
			 _context.Database.Migrate();
		}

		public void Dispose()
        {
			//_context.Dispose();

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
