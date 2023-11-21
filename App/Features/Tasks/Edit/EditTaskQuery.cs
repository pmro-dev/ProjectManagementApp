using App.Common.ViewModels;
using App.Features.Tasks.Edit.Models;
using MediatR;

namespace App.Features.Tasks.Edit;

public class EditTaskQuery : IRequest<WrapperViewModel<TaskEditInputVM, TaskEditOutputVM>>
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
