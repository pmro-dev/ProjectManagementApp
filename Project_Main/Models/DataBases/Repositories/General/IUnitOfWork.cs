using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Project_Main.Models.DataBases.Repositories.General
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();

        void SaveChanges();

		public IDbContextTransaction BeginTransaction();

        public void CommitTransaction();

        public void RollbackTransaction();

        public IEnumerable<string> GetPendingMigrations();

        public void Migrate();
	}
}
