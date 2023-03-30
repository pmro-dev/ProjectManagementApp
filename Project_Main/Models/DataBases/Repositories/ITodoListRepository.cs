using Project_DomainEntities;

namespace Project_Main.Models.DataBases.Repositories
{
    public interface ITodoListRepository : IGenericRepository<TodoListModel>
    {
        Task<TodoListModel> GetWithDetailsAsync(int id);

        Task<List<TodoListModel>> GetAllWithDetailsAsync(string userId);

        Task DuplicateWithDetailsAsync(int id);

        Task<bool> DoesAnyExistWithSameNameAsync(string name);
    }
}
