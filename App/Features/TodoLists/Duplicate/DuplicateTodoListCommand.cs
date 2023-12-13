using MediatR;

namespace App.Features.TodoLists.Duplicate;

public class DuplicateTodoListCommand : IRequest<DuplicateTodoListCommandResponse>
{
	public int TodoListId { get; }

	public DuplicateTodoListCommand(int todoListId)
	{
		TodoListId = todoListId;
	}
}

public record DuplicateTodoListCommandResponse(
	string? ErrorMessage = null, 
	int StatusCode = StatusCodes.Status201Created
){}