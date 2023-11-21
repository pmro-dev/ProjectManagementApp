using App.Common.ViewModels;
using MediatR;

namespace App.Features.TodoLists.Create;

public class CreateTodoListQuery : IRequest<WrapperViewModel<TodoListCreateInputVM, TodoListCreateOutputVM>>
{
	public string UserId { get; }

	public CreateTodoListQuery(string userId)
	{
		UserId = userId;
	}
}
