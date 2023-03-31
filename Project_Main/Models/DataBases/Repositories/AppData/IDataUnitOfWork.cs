using Project_Main.Models.DataBases.Repositories.General;

namespace Project_Main.Models.DataBases.Repositories.AppData
{
    public interface IDataUnitOfWork : IUnitOfWork
    {
        ITodoListRepository TodoListRepository { get; }
        ITaskRepository TaskRepository { get; }
    }
}
