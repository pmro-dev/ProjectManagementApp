using App.Common.Helpers;
using App.Infrastructure.Databases.Common.Interfaces;
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
		ExceptionsService.WhenModelIsNullThrowCritical(nameof(AddAsync), entity, _logger);

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
		ExceptionsService.WhenArgumentIsInvalidThrowError(nameof(GetAsync), id, nameof(id), _logger);

		return await _dbSet.FindAsync(id);
	}

	///<inheritdoc />
	public async Task<ICollection<TEntity>> GetAllByFilterAsync(Expression<Func<TEntity, bool>> filter)
	{
		ExceptionsService.ThrowErrorFilterExpressionIsNull(filter, nameof(GetAllByFilterAsync), _logger);

		return await _dbSet.Where(filter).ToListAsync();
	}

	///<inheritdoc />
	public async Task<TEntity?> GetByFilterAsync(Expression<Func<TEntity, bool>> filter)
	{
		ExceptionsService.ThrowErrorFilterExpressionIsNull(filter, nameof(GetByFilterAsync), _logger);
	
		return await _dbSet.SingleOrDefaultAsync(filter);
	}

	///<inheritdoc />
	public void Remove(TEntity entity)
	{
		ExceptionsService.WhenModelIsNullThrowCritical(nameof(Remove), entity, _logger);

		_dbSet.Remove(entity);
	}

	///<inheritdoc />
	public void Update(TEntity entity)
	{
		ExceptionsService.WhenModelIsNullThrowCritical(nameof(Update), entity, _logger);

		_dbSet.Update(entity);
	}

	///<inheritdoc />
	public async Task AddRangeAsync(ICollection<TEntity> range)
	{
		ExceptionsService.WhenModelIsNullThrowCritical(nameof(AddRangeAsync), range, _logger);

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
