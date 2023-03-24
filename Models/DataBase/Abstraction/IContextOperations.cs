using TODO_Domain_Entities;

namespace TODO_List_ASPNET_MVC.Models.DataBase.Abstraction
{
    /// <summary>
    /// Provides methods to manage database operations.
    /// </summary>
    public interface IContextOperations
    {
        /// <summary>
        /// Executes OptionAdd to database operation on TodoLists DbSet.
        /// </summary>
        /// <param name="todoList"><see cref="TodoListModel"/> object that should be added to Database.</param>
        /// <returns>Result of operation - true if succeed, false if failed.</returns>
        Task<bool> AddTodoListAsync(TodoListModel todoList);

		/// <summary>
		/// Executes OptionRemove from database operation on TodoLists DbSet.
		/// </summary>
		/// <param name="todoListId">List ID.</param>
		/// <returns>Result of operation - true if succeed, false if failed.</returns>
		Task<bool> DeleteTodoListAsync(int todoListId, string signedInUserId);

		/// <summary>
		/// Executes Get TodoLists from database operation on TodoLists DbSet.
		/// </summary>
		/// <returns>List of TodoLists.</returns>
		Task<List<TodoListModel>> GetAllTodoListsAsync(string signedInUserId);

		/// <summary>
		/// Executes Get TodoLists operation from database with details of every todolist.
		/// </summary>
		/// <returns>List of TodoLists.</returns>
		Task<List<TodoListModel>> GetAllTodoListsWithDetailsAsync(string signedInUserId);

		/// <summary>
		/// Executes Get List from database operation on TodoLists DbSet and include Tasks DbSet.
		/// </summary>
		/// <param name="todoListId">TODOList ID.</param>
		/// <returns>TODOList object with data from DataBase.</returns>
		Task<TodoListModel> GetTodoListWithDetailsAsync(int todoListId, string signedInUserId);

		/// <summary>
		/// Executes Get List from database operation on TodoLists DbSet.
		/// </summary>
		/// <param name="todoListId">TODOList ID.</param>
		/// <returns>TODOList object with data from DataBase.</returns>
		Task<TodoListModel> GetTodoListAsync(int todoListId, string signedInUserId);

		/// <summary>
		/// Executes OptionUpdate TODOList database operation on List object from TodoLists DbSet.
		/// </summary>
		/// <param name="list"><see cref="TodoListModel"/> object that we update.</param>
		/// <returns>Result of operation - true if succeed, false if failed.</returns>
		Task<bool> UpdateTodoListAsync(TodoListModel todoList);

		/// <summary>
		/// Creates a copy of existed To Do List with details and add it as new one to Database.
		/// </summary>
		/// <param name="todoListId">Target To Do List id to copy.</param>
		/// <returns>Result of operation - true if succeed, false if failed.</returns>
		Task<bool> DuplicateTodoListWithDetailsAsync(int todoListId, string signedInUserId);

		/// <summary>
		/// Checks that To Do List with given name already exists in Database.
		/// </summary>
		/// <param name="todoListName">To Do List name.</param>
		/// <returns>True when exist or false when not exist.</returns>
		Task<bool> DoesTodoListWithSameNameExist(string todoListName);

		/// <summary>
		/// Executes adding database's operation on Tasks DbSet.
		/// </summary>
		/// <param name="newTask">Task object to add to Database.</param>
		/// <returns>Result of operation - true if succeed, false if failed.</returns>
		Task<bool> CreateTaskAsync(TaskModel newTask);

		/// <summary>
		/// Executes Get Task from database.
		/// </summary>
		/// <param name="taskId">Task Id.</param>
		/// <returns>TaskModel object with data from DataBase.</returns>
		Task<TaskModel> ReadTaskAsync(int taskId, string signedInUserId);

		/// <summary>
		/// Executes updating database's operation on Tasks DbSet.
		/// </summary>
		/// <param name="taskToUpdate">Task object to update in Database.</param>
		/// <returns>Result of operation - true if succeed, false if failed.</returns>
		Task<bool> UpdateTaskAsync(TaskModel taskToUpdate);

        /// <summary>
        /// Executes deleting database operation on Tests DbSet.
        /// </summary>
        /// <param name="taskId">Id of task to remove.</param>
        /// <returns>Result of operation - true if succeed, false if failed.</returns>
        Task<bool> DeleteTaskAsync(int taskId, string signedInUserId);
	}
}
