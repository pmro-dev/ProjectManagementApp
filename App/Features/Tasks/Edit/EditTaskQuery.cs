using App.Common.ViewModels;
using App.Features.Tasks.Edit.Models;
using MediatR;

namespace App.Features.Tasks.Edit;

public class EditTaskQuery : IRequest<EditTaskQueryResponse>
{
	public int TodoListId { get; }
	public int TaskId { get; }

	public string? SignedInUserId { get; }

	public EditTaskQuery(int todoListId, int taskId, string? signedInUserId)
	{
		TodoListId = todoListId;
		TaskId = taskId;
		SignedInUserId = signedInUserId;
	}
}

public record EditTaskQueryResponse(
	WrapperViewModel<TaskEditInputVM, TaskEditOutputVM>? Data, 
	string? ErrorMessage = null, 
	int StatusCode = StatusCodes.Status200OK
){}
