using App.Infrastructure.Databases.Common.Interfaces;
using App.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Infrastructure.Databases.Common;

///<inheritdoc />
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
	protected readonly DbContext Context;
	private readonly DbSet<TEntity> _dbSet;
	private readonly ILogger<GenericRepository<TEntity>> _logger;

	public GenericRepository(DbContext dbContext, ILogger<GenericRepository<TEntity>> logger)
	{
		Context = dbContext;
		_dbSet = Context.Set<TEntity>();
		_logger = logger;
	}

	///<inheritdoc />
	public async Task AddAsync(TEntity entity)
	{
		string classAndEntityInfo = string.Concat(nameof(GenericRepository<TEntity>), typeof(TEntity));
		string operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(AddAsync), classAndEntityInfo);
		HelperCheck.ThrowExceptionWhenModelNull(operationName, entity, nameof(entity), _logger);

		await _dbSet.AddAsync(entity);
	}

	///<inheritdoc />
	public async Task<ICollection<TEntity>> GetAllAsync()
	{
		return await _dbSet.ToListAsync();
	}

	///<inheritdoc />
	public async Task<TEntity?> GetAsync(object id)
	{
		string classAndEntityInfo = string.Concat(nameof(GenericRepository<TEntity>), typeof(TEntity));
		string operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(GetAsync), classAndEntityInfo);
		HelperCheck.ThrowExceptionWhenArgumentIsNullOrEmpty(operationName, id, nameof(id), _logger);

		return await _dbSet.FindAsync(id);
	}

	///<inheritdoc />
	public async Task<ICollection<TEntity>> GetAllByFilterAsync(Expression<Func<TEntity, bool>> filter)
	{
		IQueryable<TEntity> entities = _dbSet;

		if (filter != null)
		{
			entities = entities.Where(filter);
		}

		return await entities.ToListAsync();
	}

	///<inheritdoc />
	public async Task<TEntity?> GetByFilterAsync(Expression<Func<TEntity, bool>> filter)
	{
		if (filter != null)
		{
			return await _dbSet.SingleOrDefaultAsync(filter);
		}

		return null;
	}

	///<inheritdoc />
	public void Remove(TEntity entity)
	{
		string classAndEntityInfo = string.Concat(nameof(GenericRepository<TEntity>), typeof(TEntity));
		string operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Remove), classAndEntityInfo);
		HelperCheck.ThrowExceptionWhenModelNull(operationName, entity, nameof(entity), _logger);

		_dbSet.Remove(entity);
	}

	///<inheritdoc />
	public void Update(TEntity entity)
	{
		string classAndEntityInfo = string.Concat(nameof(GenericRepository<TEntity>), typeof(TEntity));
		string operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Update), classAndEntityInfo);
		HelperCheck.ThrowExceptionWhenModelNull(operationName, entity, nameof(entity), _logger);

		_dbSet.Update(entity);
	}

	///<inheritdoc />
	public async Task AddRangeAsync(ICollection<TEntity> range)
	{
		string classAndEntityInfo = string.Concat(nameof(GenericRepository<TEntity>), typeof(TEntity));
		string operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(AddRangeAsync), classAndEntityInfo);
		HelperCheck.ThrowExceptionWhenModelNull(operationName, range, nameof(range), _logger);

		await _dbSet.AddRangeAsync(range, default);
	}

	///<inheritdoc />
	public async Task<bool> ContainsAny(Expression<Func<TEntity, bool>>? predicate = null)
	{
		if (predicate is null)
		{
			return await _dbSet.AnyAsync();
		}

		return await _dbSet.AnyAsync(predicate);
	}
}
