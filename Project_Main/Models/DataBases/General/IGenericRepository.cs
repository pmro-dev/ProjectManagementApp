using System.Linq.Expressions;

namespace Project_Main.Models.DataBases.General
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<T?> GetAsync(object id);
        Task<IEnumerable<T>> GetByFilterAsync(Expression<Func<T, bool>>? filter = null);
        Task Update(T entity);
        Task Remove(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> ContainsAny();
        Task AddRangeAsync(IEnumerable<T> range);
    }
}
