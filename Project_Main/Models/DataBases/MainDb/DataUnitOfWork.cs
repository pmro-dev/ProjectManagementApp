using Project_Main.Models.DataBases.General;

namespace Project_Main.Models.DataBases.AppData
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
