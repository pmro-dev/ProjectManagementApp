using App.Infrastructure.Databases.App.Interfaces;
using App.Infrastructure.Databases.Common;

namespace App.Infrastructure.Databases.App;

///<inheritdoc />
public class DataUnitOfWork : UnitOfWork<CustomAppDbContext>, IDataUnitOfWork
{
	///<inheritdoc />
	public ITodoListRepository TodoListRepository { get; }

	///<inheritdoc />
	public ITaskRepository TaskRepository { get; }

	public DataUnitOfWork(CustomAppDbContext context, ITodoListRepository todoListRepository, ITaskRepository taskRepository)
		: base(context)
	{
		TodoListRepository = todoListRepository;
		TaskRepository = taskRepository;
	}
}
