using System.Linq.Expressions;

namespace Project_Main.Models.DataBases.General
{
    /// <summary>
    /// Generic class for T type repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
		/// <summary>
		/// Add entity to Db by repository.
		/// </summary>
		/// <param name="entity">An entity that should be added to repository.</param>
		/// <returns>Async Task operation.</returns>
		Task AddAsync(T entity);

		/// <summary>
		/// Get entity from Db by repository.
		/// </summary>
		/// <param name="id">Id of targeted entity.</param>
		/// <returns>Async Task operation.</returns>
		Task<T?> GetAsync(object id);

		/// <summary>
		/// Get all entities that match filter, default filter is null.
		/// </summary>
		/// <param name="filter">Filter expression to filter entities in Db.</param>
		/// <returns>Async Task operation.</returns>
		Task<IEnumerable<T>> GetAllByFilterAsync(Expression<Func<T, bool>>? filter = null);

		/// <summary>
		/// Get single entity that matches filter, default filter is null.
		/// </summary>
		/// <param name="filter">Filter expression to filter entities in Db.</param>
		/// <returns>Async Task operation.</returns>
		Task<T?> GetSingleByFilterAsync(Expression<Func<T, bool>>? filter);

		/// <summary>
		/// Update entity in Db by repository.
		/// </summary>
		/// <param name="entity">Entity to update.</param>
		void Update(T entity);

		/// <summary>
		/// Remove entity from Db by repository.
		/// </summary>
		/// <param name="entity">Entity to remove.</param>
		void Remove(T entity);

		/// <summary>
		/// Get all possible entities from Db by repository.
		/// </summary>
		/// <returns>IEnumerable set of entities from Db.</returns>
		Task<IEnumerable<T>> GetAllAsync();

		/// <summary>
		/// Check that Db contains any entity.
		/// </summary>
		/// <returns>Return True when Db contains any entity, otherwise return false.</returns>
		Task<bool> ContainsAny();

		/// <summary>
		/// Add range of entities to Db by repository.
		/// </summary>
		/// <param name="range">Range of entities.</param>
		/// <returns>Async Task operation.</returns>
		Task AddRangeAsync(IEnumerable<T> range);
    }
}
