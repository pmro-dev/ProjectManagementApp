using App.Common.ViewModels;
using MediatR;

namespace App.Features.Tasks.Delete;

public class DeleteTaskQuery : IRequest<WrapperViewModel<TaskDeleteInputVM, TaskDeleteOutputVM>>
{
	public int TodoListId { get; }
	public int TaskId { get; }

	public DeleteTaskQuery(int todoListId, int taskId)
	{
		TodoListId = todoListId;
		TaskId = taskId;
	}
}
