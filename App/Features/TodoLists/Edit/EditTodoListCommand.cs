using App.Common.ViewModels;
using App.Features.TodoLists.Edit.Models;
using MediatR;

namespace App.Features.TodoLists.Edit;

public class EditTodoListCommand : IRequest<bool>
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
