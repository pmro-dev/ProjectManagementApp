using Microsoft.EntityFrameworkCore;

namespace Project_Main.Models.DataBases.Repositories
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
	}
}
