using App.Features.TodoLists.Edit.Models;
using MediatR;

namespace App.Features.TodoLists.Edit;

public class EditTodoListCommand : IRequest<EditTodoListCommandResponse>
{
	public int TodoListId { get; }
	public int RouteTodoListId { get; }

	public TodoListEditInputVM InputVM { get; }

	public EditTodoListCommand(TodoListEditInputVM inputVM, int routeTodoListId)
	{
        InputVM = inputVM;
		TodoListId = InputVM.Id;
		RouteTodoListId = routeTodoListId;
	}
}

public record EditTodoListCommandResponse(
	string? ErrorMessage = null, 
	int StatusCode = StatusCodes.Status201Created
){}