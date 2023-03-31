using Microsoft.EntityFrameworkCore;

namespace Project_Main.Models.DataBases.Repositories.General
{
    //public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    //{
    //	ITodoListRepository TodoLists { get; }
    //	ITaskRepository Tasks { get; }
    //	Task SaveChangesAsync();
    //}

    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();

        public Task BeginTransactionAsync();

        public Task CommitTransactionAsync();

        public Task RollbackTransactionAsync();

        public Task<IEnumerable<string>> GetPendingMigrationsAsync();

        public Task MigrateAsync();
	}
}
