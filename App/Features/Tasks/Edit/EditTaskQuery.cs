using App.Common.ViewModels;
using App.Features.Tasks.Edit.Models;
using MediatR;

namespace App.Features.Tasks.Edit;

public class EditTaskQuery : IRequest<EditTaskQueryResponse>
{
	public Guid TodoListId { get; }
	public Guid TaskId { get; }

	public string? SignedInUserId { get; }

	public EditTaskQuery(Guid todoListId, Guid taskId, string? signedInUserId)
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
