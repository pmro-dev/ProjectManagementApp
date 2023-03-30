namespace Project_Main.Models.DataBases.Repositories
{
	public interface IDataUnitOfWork : IUnitOfWork
	{
		ITodoListRepository TodoListRepository { get; }
		ITaskRepository TaskRepository { get; }
	}
}
