using App.Common.ViewModels;
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
