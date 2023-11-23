using App.Features.TodoLists.Delete.Models;
using MediatR;

namespace App.Features.TodoLists.Delete;

public class DeleteTodoListQuery : IRequest<DeleteTodoListQueryResponse>
{
	public int RouteTodoListId { get; }

	public DeleteTodoListQuery(int routeTodoListId)
	{
		RouteTodoListId = routeTodoListId;
	}
}

public record DeleteTodoListQueryResponse(TodoListDeleteOutputVM? Data, string? ErrorMessage = null, int StatusCode = StatusCodes.Status200OK) { }