using App.Common.ViewModels;
using App.Features.TodoLists.Create.Models;
using MediatR;

namespace App.Features.TodoLists.Create;

public class CreateTodoListCommand : IRequest<CreateTodoListCommandResponse>
{
	public WrapperViewModel<TodoListCreateInputVM, TodoListCreateOutputVM> WrapperVM { get; }

	public string UserId { get; }

	public CreateTodoListCommand(WrapperViewModel<TodoListCreateInputVM, TodoListCreateOutputVM> wrapperVM, string userId)
	{
		WrapperVM = wrapperVM;
		UserId = userId;
	}
}

public record CreateTodoListCommandResponse(string? ErrorMessage = null, int StatusCode = StatusCodes.Status201Created) { }
