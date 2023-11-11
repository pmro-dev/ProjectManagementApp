using Microsoft.EntityFrameworkCore.Storage;

namespace Web.Databases.Common.Interfaces;

/// <summary>
/// Unit Of Work class allows to manage group of Db operations at once ex. as transaction, also it's possible to get migrations and rollback transaction.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Save changes in Database made by repositories.
    /// </summary>
    /// <returns>Async Task operation.</returns>
    Task SaveChangesAsync();

    /// <summary>
    /// Begin transaction to make in Database.
    /// </summary>
    /// <returns>Database context transaction.</returns>
    public Task<IDbContextTransaction> BeginTransactionAsync();

    /// <summary>
    /// Execute transaction in Database.
    /// </summary>
    /// <returns>Async Task operation.</returns>
    public Task CommitTransactionAsync();

    /// <summary>
    /// Get back to a state before transaction began.
    /// </summary>
    /// <returns>Async Task operation.</returns>
    public Task RollbackTransactionAsync();

    /// <summary>
    /// Get any pending migration on Database.
    /// </summary>
    /// <returns>IEnumerable set of pending migrations.</returns>
    public Task<IEnumerable<string>> GetPendingMigrationsAsync();

    /// <summary>
    /// Execute migration on Database.
    /// </summary>
    /// <returns>Async Task operation.</returns>
    public Task MigrateAsync();
}
