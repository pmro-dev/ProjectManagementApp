using Project_DomainEntities;
using Project_Main.Models.DataBases.General;

namespace Project_Main.Models.DataBases.AppData
{
    public interface ITodoListRepository : IGenericRepository<TodoListModel>
    {
        Task<TodoListModel> GetWithDetailsAsync(int id);

        Task<List<TodoListModel>> GetAllWithDetailsAsync(string userId);

        Task DuplicateWithDetailsAsync(int id);

        Task<bool> DoesAnyExistWithSameNameAsync(string name);
    }
}
