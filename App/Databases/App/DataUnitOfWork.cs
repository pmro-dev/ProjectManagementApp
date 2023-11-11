using Web.Databases.App.Interfaces;
using Web.Databases.Common;
using Web.Tasks.Common.Interfaces;
using Web.TodoLists.Common.Interfaces;

namespace Web.Databases.App
{
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
}
