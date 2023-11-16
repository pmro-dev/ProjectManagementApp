using App.Features.TodoLists.Common.Models;
using App.Infrastructure.Databases.Common.Interfaces;
using System.Linq.Expressions;

namespace App.Infrastructure.Databases.App.Interfaces;

///<inheritdoc />
public interface ITodoListRepository : IGenericRepository<TodoListModel>
{
	/// <summary>
	/// Get a specific ToDoList with details (where details are related data in other tables).
	/// </summary>
	/// <param name="todoListId">Targeted list id.</param>
	/// <returns>ToDoList with details from Db.</returns>
	Task<TodoListModel?> GetWithDetailsAsync(int todoListId);

	/// <summary>
	/// Get All ToDoLists with details (where details are related data in other tables).
	/// </summary>
	/// <param name="userId"></param>
	/// <returns>All ToDoLists wiith details from Db.</returns>
	Task<ICollection<TodoListModel>> GetAllWithDetailsAsync(string userId);

	/// <summary>
	/// Get only those TodoLists with details (where details are related data in other tables) which follow predicate filter.
	/// </summary>
	/// <param name="filter"></param>
	/// <returns></returns>
	Task<ICollection<TodoListModel>> GetAllWithDetailsByFilterAsync(Expression<Func<TodoListModel, bool>> filter);

	/// <summary>
	/// Duplicate a whole, specifc ToDoList with details in Db (where details are related data in other tables).
	/// </summary>
	/// <param name="todoListId">Targeted list id.</param>
	/// <returns></returns>
	Task DuplicateWithDetailsAsync(int todoListId);

	/// <summary>
	/// Check that any ToDoList with the same name already exists.
	/// </summary>
	/// <param name="todoListName">Targeted name to check.</param>
	/// <returns>True when ToDoList with specified name already exists, otherwise false.</returns>
	Task<bool> CheckThatAnyWithSameNameExistAsync(string todoListName);
}
