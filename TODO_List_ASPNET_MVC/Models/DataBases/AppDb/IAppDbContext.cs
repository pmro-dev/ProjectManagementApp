using Microsoft.EntityFrameworkCore;
using Project_DomainEntities;

namespace Project_Main.Models.DataBases.AppDb
{
    /// <summary>
    /// Interface for DbContext with required methods and DbSets.
    /// </summary>
    public interface IAppDbContext
    {
        /// <summary>
        /// Returns DbSet with Tasks.
        /// </summary>
        DbSet<TaskModel> Tasks => Set<TaskModel>();

        /// <summary>
        /// Returns DbSet with To Do Lists.
        /// </summary>
        DbSet<TodoListModel> TodoLists => Set<TodoListModel>();

        /// <summary>
        /// Give access to TaskTags DbSet.
        /// </summary>
        DbSet<TaskTagModel> TaskTags => Set<TaskTagModel>();

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Reads To Do List with details from Database.
        /// </summary>
        Task<TodoListModel> GetTodoListWithDetailsAsync(int todoListId, string signedInUserId);

        /// <summary>
        /// Creates To Do List and add to Database.
        /// </summary>
        Task<int> AddTodoListAsync(TodoListModel newTodoList);

        /// <summary>
        /// Reads All To Do List from Database.
        /// </summary>
        Task<List<TodoListModel>> GetAllTodoListsAsync(string signedInUserId);

        /// <summary>
        /// Reads All To Do Lists with details from Database.
        /// </summary>
        Task<List<TodoListModel>> GetAllTodoListsWithDetailsAsync(string signedInUserId);

        /// <summary>
        /// Reads To Do List from Database.
        /// </summary>
        Task<TodoListModel> GetTodoListAsync(int todoListId, string signedInUserId);

        /// <summary>
        /// Updates To Do List data in Database.
        /// </summary>
        Task<int> UpdateTodoListAsync(TodoListModel todoListToUpdate);

        /// <summary>
        /// Deletes To Do List from Database.
        /// </summary>
        Task<int> DeleteTodoListAsync(TodoListModel todoListToDelete);

        /// <summary>
        /// Checks that To Do List with given name already exists in Database.
        /// </summary>
        /// <param name="todoListName">To Do List name.</param>
        /// <returns>True when exist or false when not exist.</returns>
        Task<bool> DoesTodoListExistByName(string todoListName);

        /// <summary>
        /// Creates Task and add it to Database.
        /// </summary>
        Task<int> CreateTaskAsync(TaskModel newTask);

        /// <summary>
        /// Reads Task from Database.
        /// </summary>
        Task<TaskModel> ReadTaskAsync(int taskId, string signedInUserId);

        /// <summary>
        /// Updates Task in Database.
        /// </summary>
        Task<int> UpdateTaskAsync(TaskModel taskToUpdate);

        /// <summary>
        /// Delete Task from Database.
        /// </summary>
        Task<int> DeleteTaskAsync(TaskModel taskToDelete);

        /// <summary>
        /// Saves all changes for tracking object.
        /// </summary>
        Task<int> SaveChangesAsync();
    }
}
