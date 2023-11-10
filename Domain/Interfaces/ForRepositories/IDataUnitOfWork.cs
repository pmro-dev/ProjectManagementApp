using Project_Main.Models.DataBases.General;

namespace Project_Main.Models.DataBases.AppData
{
	///<inheritdoc />
	public interface IDataUnitOfWork : IUnitOfWork
    {
		///<inheritdoc />
		ITodoListRepository TodoListRepository { get; }

		///<inheritdoc />
		ITaskRepository TaskRepository { get; }
    }
}
