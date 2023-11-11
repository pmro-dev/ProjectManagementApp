using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Web.Databases.Common.Interfaces;

namespace Web.Databases.Common
{
	///<inheritdoc />
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
			try
			{
				await _context.SaveChangesAsync();
			}
			catch
			{
				await _context.Database.RollbackTransactionAsync();
			}
		}

		public async Task<IDbContextTransaction> BeginTransactionAsync()
		{
			return await _context.Database.BeginTransactionAsync();
		}

		public async Task CommitTransactionAsync()
		{
			try
			{
				await _context.Database.CommitTransactionAsync();

			}
			catch
			{
				await _context.Database.RollbackTransactionAsync();
			}
		}

		public async Task RollbackTransactionAsync()
		{
			await _context.Database.RollbackTransactionAsync();
		}

		public async Task<IEnumerable<string>> GetPendingMigrationsAsync()
		{
			return await _context.Database.GetPendingMigrationsAsync();
		}

		public async Task MigrateAsync()
		{
			await _context.Database.MigrateAsync();
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
