using Web.Databases.Common.Interfaces;
using Web.Tasks.Common.Interfaces;
using Web.TodoLists.Common.Interfaces;

namespace Web.Databases.App.Interfaces
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
