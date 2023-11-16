using App.Infrastructure.Databases.App.Interfaces;
using App.Infrastructure.Databases.Common;

namespace App.Infrastructure.Databases.App;

///<inheritdoc />
public class DataUnitOfWork : UnitOfWork<CustomAppDbContext>, IDataUnitOfWork
{
	protected readonly ILogger<DataUnitOfWork> _logger;

	///<inheritdoc />
	public ITodoListRepository TodoListRepository { get; }

	///<inheritdoc />
	public ITaskRepository TaskRepository { get; }

	public DataUnitOfWork(CustomAppDbContext context, ITodoListRepository todoListRepository, ITaskRepository taskRepository, ILogger<DataUnitOfWork> logger)
		: base(context, logger)
	{
		TodoListRepository = todoListRepository;
		TaskRepository = taskRepository;
		_logger = logger;
	}
}
