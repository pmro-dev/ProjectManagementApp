using Microsoft.EntityFrameworkCore;

namespace Project_Main.Models.DataBases.Repositories.General
{
    //public abstract class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext, new()
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

    //public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext, new()
    //{
    //	private readonly TContext _dbContext;
    //	private readonly ILoggerFactory _loggerFactory;
    //	private bool _disposed;

    //	public ITodoListRepository TodoLists { get; private set; }

    //	public ITaskRepository Tasks { get; private set; }

    //	public UnitOfWork(TContext dbContext, ILoggerFactory loggerFactory)
    //	{
    //		_dbContext = dbContext;
    //		_loggerFactory = loggerFactory;
    //		TodoLists = new TodoListRepository(_dbContext, _loggerFactory.CreateLogger<TodoListRepository<TodoListModel>>());
    //		TodoLists = new TaskRepository(_dbContext);
    //	}

    //	//public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    //	//{
    //	//	//if (_repositories.ContainsKey(typeof(TEntity)))
    //	//	//{
    //	//	//	return (IGenericRepository<TEntity>)_repositories[typeof(TEntity)];
    //	//	//}

    //	//	//var repository = new GenericRepository<TEntity>(_dbContext, _loggerFactory.CreateLogger<GenericRepository<TEntity>>());
    //	//	//_repositories.Add(typeof(TEntity), repository);
    //	//	//return repository;

    //	//	var repositoryType = typeof(GenericRepository<>);
    //	//	var repositoryInstanceType = repositoryType.MakeGenericType(typeof(TEntity));
    //	//	var repositoryInstance = Activator.CreateInstance(repositoryInstanceType, _dbContext);
    //	//	return (IGenericRepository<TEntity>)repositoryInstance;
    //	//}

    //	public void SaveChanges()
    //	{
    //		_dbContext.SaveChanges();
    //	}

    //	public async Task SaveChangesAsync()
    //	{
    //		await _dbContext.SaveChangesAsync();
    //	}

    //	public void Dispose()
    //	{
    //		Dispose(true);
    //		GC.SuppressFinalize(this);
    //	}

    //	protected virtual void Dispose(bool disposing)
    //	{
    //		if (!_disposed)
    //		{
    //			if (disposing)
    //			{
    //				_dbContext.Dispose();
    //			}

    //			_disposed = true;
    //		}
    //	}
    //}
}
