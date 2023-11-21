using App.Common.ViewModels;
using App.Features.TodoLists.Edit.Models;
using MediatR;

namespace App.Features.TodoLists.Edit;

public class EditTodoListQuery : IRequest<WrapperViewModel<TodoListEditInputVM, TodoListEditOutputVM>>
{
	public int TodoListId { get; }

	public EditTodoListQuery(int todoListId)
	{
		TodoListId = todoListId;
	}
}
