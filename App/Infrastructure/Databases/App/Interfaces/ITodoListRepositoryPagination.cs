using App.Features.TodoLists.Common.Models;
using System.Linq.Expressions;

namespace App.Infrastructure.Databases.App.Interfaces;

public interface ITodoListRepositoryPagination
{
	Task<TodoListModel?> GetSingleWithDetailsAsync(int todoListId, int pageNumber, int itemsPerPageCount);
	Task<ICollection<TodoListModel>> GetMultipleWithDetailsAsync(string userId, Expression<Func<TodoListModel, object>> orderBySelector, int pageNumber, int itemsPerPageCount);
	IQueryable<TodoListModel> GetMultipleByFilter(Expression<Func<TodoListModel, bool>> filter, Expression<Func<TodoListModel, object>> orderBySelector, int pageNumber, int itemsPerPageCount);
}
