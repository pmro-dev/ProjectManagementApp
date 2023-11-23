using App.Common.ViewModels;
using App.Features.TodoLists.Edit.Models;
using MediatR;

namespace App.Features.TodoLists.Edit;

public class EditTodoListCommand : IRequest<EditTodoListCommandResponse>
{
	public int TodoListId { get; }
	public int RouteTodoListId { get; }

	public WrapperViewModel<TodoListEditInputVM, TodoListEditOutputVM> WrapperVM { get; }

	public EditTodoListCommand(int todoListId, WrapperViewModel<TodoListEditInputVM, TodoListEditOutputVM> wrapperVM, int routeTodoListId)
	{
		TodoListId = todoListId;
		WrapperVM = wrapperVM;
		RouteTodoListId = routeTodoListId;
	}
}

public record EditTodoListCommandResponse(string? ErrorMessage = null, int StatusCode = StatusCodes.Status201Created) { }