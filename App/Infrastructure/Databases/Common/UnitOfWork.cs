using App.Features.Exceptions.Throw;
using App.Infrastructure.Databases.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace App.Infrastructure.Databases.Common;

///<inheritdoc />
public abstract class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
	protected readonly TContext _context;
	private bool _disposed = false;
	private readonly ILogger<UnitOfWork<TContext>> _logger;

	protected UnitOfWork(TContext context, ILogger<UnitOfWork<TContext>> logger)
	{
		_context = context;
		_logger = logger;
	}

	public virtual async Task SaveChangesAsync()
	{
		try
		{
			await _context.SaveChangesAsync();
		}
		catch
		{
			if (_context.Database.CurrentTransaction is null)
			{
				_logger.LogWarning(ExceptionsMessages.LogExceptionOccuredOnSavingDataToDataBase, nameof(SaveChangesAsync));
				_context.ChangeTracker.Clear();
				throw;
			}
			else
			{
				_logger.LogCritical(ExceptionsMessages.LogExceptionOccuredOnSavingDataToDataBase, nameof(SaveChangesAsync));
				await RollbackTransactionAsync();
				throw;
			}
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
			_logger.LogCritical(ExceptionsMessages.LogExceptionOccuredOnCommitingTransaction, nameof(CommitTransactionAsync));
			await RollbackTransactionAsync();
			throw;
		}
	}

	public async Task RollbackTransactionAsync()
	{
		_logger.LogWarning(ExceptionsMessages.LogRollbackProceed);
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
