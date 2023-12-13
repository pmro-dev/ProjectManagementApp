using MediatR;

namespace App.Features.TodoLists.Delete;

public class DeleteTodoListCommand : IRequest<DeleteTodoListCommandResponse>
{
	public int TodoListId { get; }

	public DeleteTodoListCommand(int todoListId)
	{
		TodoListId = todoListId;
	}
}

public record DeleteTodoListCommandResponse(
	string? ErrorMessage = null, 
	int StatusCode = StatusCodes.Status200OK
){}