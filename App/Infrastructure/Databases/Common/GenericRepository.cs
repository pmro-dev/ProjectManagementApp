﻿using App.Features.Exceptions.Throw;
using App.Features.Pagination;
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
		ExceptionsService.WhenEntityIsNullThrowCritical(nameof(AddAsync), entity, _logger);

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
		ExceptionsService.WhenFilterExpressionIsNullThrow(filter, nameof(GetAllByFilterAsync), _logger);

		return await _dbSet.Where(filter).ToListAsync();
	}

	///<inheritdoc />
	public async Task<TEntity?> GetByFilterAsync(Expression<Func<TEntity, bool>> filter)
	{
		ExceptionsService.WhenFilterExpressionIsNullThrow(filter, nameof(GetByFilterAsync), _logger);

		return await _dbSet.SingleOrDefaultAsync(filter);
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
	public async Task<bool> ContainsAny(Expression<Func<TEntity, bool>>? predicate = null)
	{
		if (predicate is null)
		{
			return await _dbSet.AnyAsync();
		}

		return await _dbSet.AnyAsync(predicate);
	}

	public async Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, object>> orderBySelector, int pageNumber, int itemsPerPageCount)
	{
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(GetAllAsync), pageNumber, nameof(pageNumber), _logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(GetAllAsync), itemsPerPageCount, nameof(itemsPerPageCount), _logger);

		var baseSource = _dbSet;
		var paginatedSource = baseSource.UsePagination(orderBySelector, pageNumber, itemsPerPageCount, _logger);

		return await paginatedSource.ToListAsync();
	}

	public async Task<ICollection<TEntity>> GetAllByFilterAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBySelector, int pageNumber, int itemsPerPageCount)
	{
		ExceptionsService.WhenFilterExpressionIsNullThrow(filter, nameof(GetAllByFilterAsync), _logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(GetAllByFilterAsync), pageNumber, nameof(pageNumber), _logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(GetAllByFilterAsync), itemsPerPageCount, nameof(itemsPerPageCount), _logger);

		var baseSource = _dbSet.Where(filter);
		var paginatedSource = baseSource.UsePagination(orderBySelector, pageNumber, itemsPerPageCount, _logger);

		return await paginatedSource.ToListAsync();
	}

	public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
	{
		if (predicate is null)
			return await _dbSet.CountAsync();

		return await _dbSet.CountAsync(predicate);
	}
}
