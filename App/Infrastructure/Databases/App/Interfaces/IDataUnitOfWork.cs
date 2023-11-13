using App.Infrastructure.Databases.Common.Interfaces;

namespace App.Infrastructure.Databases.App.Interfaces;

///<inheritdoc />
public interface IDataUnitOfWork : IUnitOfWork
{
	///<inheritdoc />
	ITodoListRepository TodoListRepository { get; }

	///<inheritdoc />
	ITaskRepository TaskRepository { get; }
}
