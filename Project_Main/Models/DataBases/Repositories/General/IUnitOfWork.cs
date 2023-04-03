using Microsoft.EntityFrameworkCore.Storage;

namespace Project_Main.Models.DataBases.Repositories.General
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();


		public Task<IDbContextTransaction> BeginTransactionAsync();

		public Task CommitTransactionAsync();

		public Task RollbackTransactionAsync();

		public Task<IEnumerable<string>> GetPendingMigrationsAsync();

		public Task MigrateAsync();
	}
}
