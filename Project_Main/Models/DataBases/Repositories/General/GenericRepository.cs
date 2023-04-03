using Microsoft.EntityFrameworkCore;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.DataBases.Repositories.Identity;
using System.Linq.Expressions;

namespace Project_Main.Models.DataBases.Repositories.General
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
	{
		protected readonly DbContext Context;
		private readonly DbSet<TEntity> _dbSet;
		private readonly ILogger<GenericRepository<TEntity>> _logger;
		private string operationName = string.Empty;

		public GenericRepository(DbContext dbContext, ILogger<GenericRepository<TEntity>> logger)
		{
			Context = dbContext;
			_dbSet = Context.Set<TEntity>();
			_logger = logger;
		}

		public async Task AddAsync(TEntity entity)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(AddAsync), nameof(UserRepository));
			HelperCheck.IfArgumentModelNullThrowException(operationName, entity, nameof(entity), _logger);
			await _dbSet.AddAsync(entity);
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<TEntity?> GetAsync(object id)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(GetAsync), nameof(UserRepository));
			HelperCheck.IfArgumentModelNullThrowException(operationName, id, nameof(id), _logger);

			return await _dbSet.FindAsync(id);
		}

		public async Task<IEnumerable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>>? filter = null)
		{
			IQueryable<TEntity> entities = _dbSet;

			if (filter != null)
			{
				entities = entities.Where(filter);
			}

			return await entities.ToListAsync();
		}

		public Task Remove(TEntity entity)
		{
			return Task.Run(() =>
			{
				operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Remove), nameof(UserRepository));
				HelperCheck.IfArgumentModelNullThrowException(operationName, entity, nameof(entity), _logger);

				_dbSet.Remove(entity);
			});
		}

		public Task Update(TEntity entity)
		{
			return Task.Run(() =>
			{
				operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Update), nameof(UserRepository));
				HelperCheck.IfArgumentModelNullThrowException(operationName, entity, nameof(entity), _logger);

				_dbSet.Update(entity);
			});
		}

		public async Task AddRangeAsync(IEnumerable<TEntity> range)
		{
			await _dbSet.AddRangeAsync(range);
		}

		public async Task<bool> ContainsAny()
		{
			return await _dbSet.AnyAsync();
		}
	}
}
