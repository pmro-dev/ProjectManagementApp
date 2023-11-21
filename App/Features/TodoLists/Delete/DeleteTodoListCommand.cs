using MediatR;

namespace App.Features.TodoLists.Delete;

public class DeleteTodoListCommand : IRequest<bool>
{
	public int TodoListId { get; }

	public DeleteTodoListCommand(int todoListId)
	{
		TodoListId = todoListId;
	}
}
