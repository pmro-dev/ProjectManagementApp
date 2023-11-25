using App.Features.TodoLists.Create.Models;
using MediatR;

namespace App.Features.TodoLists.Create;

public class CreateTodoListCommand : IRequest<CreateTodoListCommandResponse>
{
	public TodoListCreateInputVM InputVM { get; }

	public CreateTodoListCommand(TodoListCreateInputVM inputVM)
	{
        InputVM = inputVM;
	}
}

public record CreateTodoListCommandResponse(string? ErrorMessage = null, int StatusCode = StatusCodes.Status201Created) { }
