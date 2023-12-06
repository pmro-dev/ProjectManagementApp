using App.Features.Exceptions.Throw;
using App.Infrastructure.Databases.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Infrastructure.Databases.Common;

///<inheritdoc />
public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId> where TEntity : class, IBaseEntity<TId> where TId : notnull
{
	protected readonly DbContext Context;
	private readonly DbSet<TEntity> _dbSet;
	private readonly ILogger<GenericRepository<TEntity, TId>> _logger;

	public GenericRepository(DbContext dbContext, ILogger<GenericRepository<TEntity, TId>> logger)
	{
		Context = dbContext;
		_dbSet = Context.Set<TEntity>();
		_logger = logger;
	}

	///<inheritdoc />
	public async Task AddAsync(TEntity entity)
	{
		ExceptionsService.WhenEntityIsNullThrowCritical(nameof(AddAsync), entity, _logger);

		await _dbSet.AddAsync(entity);
	}

	///<inheritdoc />
	public IQueryable<TEntity> GetAll()
	{
		return _dbSet.AsNoTracking();
	}

	///<inheritdoc />
	public IQueryable<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> filter)
	{
		ExceptionsService.WhenFilterExpressionIsNullThrow(filter, nameof(GetAllByFilter), _logger);

		return _dbSet
			.AsNoTracking()
			.Where(filter);
	}

	///<inheritdoc />
	public IQueryable<TEntity?> GetEntity(object id)
	{
		ExceptionsService.WhenArgumentIsInvalidThrowError(nameof(GetEntity), id, nameof(id), _logger);

		return _dbSet.Where(e => e.Id.Equals(id));
	}

	///<inheritdoc />
	public IQueryable<TEntity?> GetByFilter(Expression<Func<TEntity, bool>> filter)
	{
		ExceptionsService.WhenFilterExpressionIsNullThrow(filter, nameof(GetByFilter), _logger);

		return _dbSet.Where(filter);
	}

	///<inheritdoc />
	public void Remove(TEntity entity)
	{
		ExceptionsService.WhenEntityIsNullThrowCritical(nameof(Remove), entity, _logger);

		_dbSet.Remove(entity);
	}

	///<inheritdoc />
	public void Update(TEntity entity)
	{
		ExceptionsService.WhenEntityIsNullThrowCritical(nameof(Update), entity, _logger);

		_dbSet.Update(entity);
	}

	///<inheritdoc />
	public async Task AddRangeAsync(ICollection<TEntity> range)
	{
		ExceptionsService.WhenEntityIsNullThrowCritical(nameof(AddRangeAsync), range, _logger);

		await _dbSet.AddRangeAsync(range, default);
	}

	///<inheritdoc />
	public async Task<bool> ContainsAnyAsync(Expression<Func<TEntity, bool>>? predicate = null)
	{
		if (predicate is null)
		{
			return await _dbSet.AnyAsync();
		}

		return await _dbSet.AnyAsync(predicate);
	}
}
