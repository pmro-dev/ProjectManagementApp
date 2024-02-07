using MediatR;

namespace App.Features.TodoLists.Duplicate;

public class DuplicateTodoListCommand : IRequest<DuplicateTodoListCommandResponse>
{
	public Guid TodoListId { get; }

	public DuplicateTodoListCommand(Guid todoListId)
	{
		TodoListId = todoListId;
	}
}

public record DuplicateTodoListCommandResponse(
	string? ErrorMessage = null, 
	int StatusCode = StatusCodes.Status201Created
){}