using Project_DomainEntities;
using Project_Main.Models.DataBases.Repositories.General;

namespace Project_Main.Models.DataBases.Repositories.AppData
{
    public interface ITodoListRepository : IGenericRepository<TodoListModel>
    {
        Task<TodoListModel> GetWithDetailsAsync(int id);

        Task<List<TodoListModel>> GetAllWithDetailsAsync(string userId);

        Task DuplicateWithDetailsAsync(int id);

        Task<bool> DoesAnyExistWithSameNameAsync(string name);

        //Task<bool> ContainsAny();

        //Task AddRangeAsync(IEnumerable<TodoListModel> todoListsRange);
	}
}
