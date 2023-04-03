using Project_Main.Models.DataBases.General;

namespace Project_Main.Models.DataBases.AppData
{
    public interface IDataUnitOfWork : IUnitOfWork
    {
        ITodoListRepository TodoListRepository { get; }
        ITaskRepository TaskRepository { get; }
    }
}
