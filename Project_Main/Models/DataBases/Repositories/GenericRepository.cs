using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Project_Main.Models.DataBases.Repositories
{
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

		public async Task AddAsync(TEntity entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<TEntity?> GetAsync(object id)
		{
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

		public void Remove(TEntity entity)
		{
			_dbSet.Remove(entity);
		}

		public void Update(TEntity entity)
		{
			_dbSet.Update(entity);
		}
	}
}
