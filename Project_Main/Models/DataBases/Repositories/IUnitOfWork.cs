namespace Project_Main.Models.DataBases.Repositories
{
	public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
		Task<int> SaveChangesAsync();
	}
}
