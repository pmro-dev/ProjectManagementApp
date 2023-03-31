using Project_Main.Models.DataBases.Repositories.General;

namespace Project_Main.Models.DataBases.Repositories.AppData
{
    public class DataUnitOfWork : UnitOfWork<CustomAppDbContext>, IDataUnitOfWork
    {
        public ITodoListRepository TodoListRepository { get; }

        public ITaskRepository TaskRepository { get; }

        public DataUnitOfWork(CustomAppDbContext context, ITodoListRepository todoListRepository, ITaskRepository taskRepository)
            : base(context)
        {
            TodoListRepository = todoListRepository;
            TaskRepository = taskRepository;
        }
    }
}
