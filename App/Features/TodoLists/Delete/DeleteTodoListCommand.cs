using MediatR;

namespace App.Features.TodoLists.Delete;

public class DeleteTodoListCommand : IRequest<DeleteTodoListCommandResponse>
{
	public Guid TodoListId { get; }

	public DeleteTodoListCommand(Guid todoListId)
	{
		TodoListId = todoListId;
	}
}

public record DeleteTodoListCommandResponse(
	string? ErrorMessage = null, 
	int StatusCode = StatusCodes.Status200OK
){}