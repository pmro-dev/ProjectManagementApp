using App.Common.ViewModels;
using App.Features.TodoLists.Edit.Models;
using MediatR;

namespace App.Features.TodoLists.Edit;

public class EditTodoListQuery : IRequest<EditTodoListQueryResponse>
{
	public Guid TodoListId { get; }

	public EditTodoListQuery(Guid todoListId)
	{
		TodoListId = todoListId;
	}
}

public record EditTodoListQueryResponse(WrapperViewModel<TodoListEditInputVM, TodoListEditOutputVM>? Data, string? ErrorMessage = null, int StatusCode = StatusCodes.Status200OK) { }