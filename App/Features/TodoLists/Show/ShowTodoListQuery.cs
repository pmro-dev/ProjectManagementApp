using App.Features.TodoLists.Show.Models;
using MediatR;

namespace App.Features.TodoLists.Show;

public class ShowTodoListQuery : IRequest<ShowTodoListQueryResponse>
{
	public int TodoListId { get; }
	public DateTime? FilterDueDate { get; }
	public int PageNumber { get; }
	public int ItemsPerPageCount { get; }

	public ShowTodoListQuery(int todoListId, DateTime? filterDueDate, int pageNumber, int itemsPerPageCount)
	{
		TodoListId = todoListId;
		FilterDueDate = filterDueDate;
		ItemsPerPageCount = itemsPerPageCount;
		PageNumber = pageNumber;
	}
}

public record ShowTodoListQueryResponse(TodoListDetailsOutputVM? Data, string? ErrorMessage = null, int StatusCode = StatusCodes.Status200OK) { }
