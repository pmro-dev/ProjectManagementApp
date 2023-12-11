using App.Features.Tasks.Common.Models;
using App.Features.TodoLists.Common.Models;
using System.Linq.Expressions;

namespace App.Infrastructure.Databases.App.Interfaces;

public interface ITodoListRepositoryPagination
{
	Task<TodoListModel?> GetSingleWithDetailsAsync(int todoListId, Func<TaskModel, object> orderDetailsBySelector, int pageNumber, int itemsPerPageCount);
	Task<ICollection<TodoListModel>> GetMultipleWithDetailsAsync(string userId, int pageNumber, int itemsPerPageCount, Expression<Func<TodoListModel, object>> orderBySelector, Expression<Func<TaskModel, object>> orderDetailsBySelector);
	IQueryable<TodoListModel> GetMultipleByFilter(Expression<Func<TodoListModel, bool>> filter, Expression<Func<TodoListModel, object>> orderBySelector, int pageNumber, int itemsPerPageCount);
}
