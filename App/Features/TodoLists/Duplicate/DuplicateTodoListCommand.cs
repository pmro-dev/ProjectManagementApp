using MediatR;

namespace App.Features.TodoLists.Duplicate;

public class DuplicateTodoListCommand : IRequest<bool>
{
	public int TodoListId { get; }

	public DuplicateTodoListCommand(int todoListId)
	{
		TodoListId = todoListId;
	}
}
