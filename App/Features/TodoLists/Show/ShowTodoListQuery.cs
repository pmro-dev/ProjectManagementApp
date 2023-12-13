using App.Features.Tasks.Common.Models;
using App.Features.TodoLists.Show.Models;
using MediatR;
using System.Linq.Expressions;

namespace App.Features.TodoLists.Show;

public class ShowTodoListQuery : IRequest<ShowTodoListQueryResponse>
{
	public int TodoListId { get; }
	public DateTime? FilterDueDate { get; }
	public int PageNumber { get; }
	public int ItemsPerPageCount { get; }
	public Expression<Func<TaskModel, object>> OrderDetailsBySelector { get; }

	public ShowTodoListQuery(int todoListId, DateTime? filterDueDate, Expression<Func<TaskModel, object>> orderDetailsBySelector, 
		int pageNumber, int itemsPerPageCount)
	{
		TodoListId = todoListId;
		FilterDueDate = filterDueDate;
		OrderDetailsBySelector = orderDetailsBySelector;
		ItemsPerPageCount = itemsPerPageCount;
		PageNumber = pageNumber;
	}
}

public record ShowTodoListQueryResponse(
	TodoListDetailsOutputVM? Data, 
	string? ErrorMessage = null, 
	int StatusCode = StatusCodes.Status200OK
){}
