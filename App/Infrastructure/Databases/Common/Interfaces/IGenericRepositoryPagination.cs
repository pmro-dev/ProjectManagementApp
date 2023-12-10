using System.Linq.Expressions;

namespace App.Infrastructure.Databases.Common.Interfaces;

public interface IGenericRepositoryPagination<TEntity> where TEntity : class
{
	Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, object>> orderBySelector, int pageNumber, int itemsPerPageCount);
	Task<ICollection<TEntity>> GetAllByFilterAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBySelector, int pageNumber, int itemsPerPageCount);
}
