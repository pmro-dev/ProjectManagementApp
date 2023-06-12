using Microsoft.EntityFrameworkCore;
using Project_DomainEntities;
using Project_Main.Infrastructure.Helpers;

namespace Project_Main.Models.DataBases.Helpers
{
    /// <summary>
    /// Class contains Db Set validators.
    /// </summary>
    public static class DbContextValidators
    {
        /// <summary>
        /// Validates that Db Set is null and throw exception when it is.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="dbSet">Specific Database Set.</param>
        /// <param name="_logger">Logger object.</param>
        /// <param name="operationName">Name of action or operation where this action is executed.</param>
        /// <exception cref="InvalidOperationException">Occurs with specific message when Db Set is null.</exception>
        public static void CheckDbSetIfNullThrowException<T>(DbSet<T> dbSet, ILogger _logger, string operationName) where T : class
        {
            if (dbSet is null)
            {
                _logger.LogCritical(Messages.LogParamNullOrEmpty, operationName, typeof(T).Name);
                throw new InvalidOperationException(Messages.DbSetIsNull);
            }
        }

		/// <summary>
		/// Validates that an entity with a specific id exists in Db Set.
		/// </summary>
		/// <typeparam name="T">Entity type.</typeparam>
		/// <param name="dbSet">Specific Database Set.</param>
		/// <param name="dbSetName">Specific Database Set's name.</param>
		/// <param name="entityId">Id of targeted entity.</param>
		/// <param name="operationName">Name of action or operation where this action is executed.</param>
		/// <param name="_logger">Logger object.</param>
		/// <returns>Async Task operation.</returns>
		/// <exception cref="InvalidOperationException"></exception>
		public static async Task CheckItemExistsIfNotThrowException<T>(DbSet<T> dbSet, string dbSetName, int entityId, string operationName, ILogger _logger) where T : BasicModelAbstract
        {
            if (await dbSet.AnyAsync(x => x.Id == entityId) is false)
            {
                _logger.LogInformation(Messages.LogEntityNotFoundInDbSet, operationName, dbSetName, entityId);
                throw new InvalidOperationException(Messages.ItemNotFoundInDb);
            }
        }
    }
}
