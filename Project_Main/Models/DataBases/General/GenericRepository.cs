using Microsoft.EntityFrameworkCore;
using Project_Main.Infrastructure.Helpers;
using System.Linq.Expressions;

namespace Project_Main.Models.DataBases.General
{
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
			HelperCheck.IfModelNullThrowException(operationName, entity, nameof(entity), _logger);

			await _dbSet.AddAsync(entity);
		}

		///<inheritdoc />
		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		///<inheritdoc />
		public async Task<TEntity?> GetAsync(object id)
		{
			string classAndEntityInfo = string.Concat(nameof(GenericRepository<TEntity>), typeof(TEntity));
			string operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(GetAsync), classAndEntityInfo);
			HelperCheck.IfArgumentIsNullOrEmptyThrowException(operationName, id, nameof(id), _logger);

			return await _dbSet.FindAsync(id);
		}

		///<inheritdoc />
		public async Task<IEnumerable<TEntity>> GetAllByFilterAsync(Expression<Func<TEntity, bool>>? filter = null)
		{
			IQueryable<TEntity> entities = _dbSet;

			if (filter != null)
			{
				entities = entities.Where(filter);
			}

			return await entities.ToListAsync();
		}

		///<inheritdoc />
		public async Task<TEntity?> GetSingleByFilterAsync(Expression<Func<TEntity, bool>>? filter)
		{
			if (filter != null)
			{
				return await _dbSet.SingleOrDefaultAsync(filter);
			}

			return null;
		}

		///<inheritdoc />
		public Task Remove(TEntity entity)
		{
			return Task.Run(() =>
			{
				string classAndEntityInfo = string.Concat(nameof(GenericRepository<TEntity>), typeof(TEntity));
				string operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Remove), classAndEntityInfo);
				HelperCheck.IfModelNullThrowException(operationName, entity, nameof(entity), _logger);

				_dbSet.Remove(entity);
			});
		}

		///<inheritdoc />
		public Task Update(TEntity entity)
		{
			return Task.Run(() =>
			{
				string classAndEntityInfo = string.Concat(nameof(GenericRepository<TEntity>), typeof(TEntity));
				string operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Update), classAndEntityInfo);
				HelperCheck.IfModelNullThrowException(operationName, entity, nameof(entity), _logger);

				_dbSet.Update(entity);
			});
		}

		///<inheritdoc />
		public async Task AddRangeAsync(IEnumerable<TEntity> range)
		{
			string classAndEntityInfo = string.Concat(nameof(GenericRepository<TEntity>), typeof(TEntity));
			string operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(AddRangeAsync), classAndEntityInfo);
			HelperCheck.IfModelNullThrowException(operationName, range, nameof(range), _logger);

			await _dbSet.AddRangeAsync(range, default);
		}

		///<inheritdoc />
		public async Task<bool> ContainsAny()
		{
			return await _dbSet.AnyAsync();
		}
	}
}
