using App.Features.TodoLists.Show.Models;
using MediatR;

namespace App.Features.TodoLists.Show;

public class ShowTodoListQuery : IRequest<ShowTodoListQueryResponse>
{
	public int TodoListId { get; }
	public DateTime? FilterDueDate { get; }

	public ShowTodoListQuery(int todoListId, DateTime? filterDueDate)
	{
		TodoListId = todoListId;
		FilterDueDate = filterDueDate;
	}
}

public record ShowTodoListQueryResponse(TodoListDetailsOutputVM? Data, string? ErrorMessage = null, int StatusCode = StatusCodes.Status200OK) { }
