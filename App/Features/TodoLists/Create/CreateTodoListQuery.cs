using App.Common.ViewModels;
using App.Features.TodoLists.Create.Models;
using MediatR;

namespace App.Features.TodoLists.Create;

public class CreateTodoListQuery : IRequest<CreateTodoListQueryResponse>
{
	public string UserId { get; }

	public CreateTodoListQuery(string userId)
	{
		UserId = userId;
	}
}

public record CreateTodoListQueryResponse(
	WrapperViewModel<TodoListCreateInputVM, TodoListCreateOutputVM>? Data, 
	string? ErrorMessage = null, 
	int StatusCode = StatusCodes.Status200OK
){}