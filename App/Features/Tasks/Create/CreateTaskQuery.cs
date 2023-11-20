using App.Common.ViewModels;
using MediatR;

namespace App.Features.Tasks.Create;

public class CreateTaskQuery : IRequest<WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM>>
{
    public int TaskId { get; }

    public CreateTaskQuery(int taskId)
    {
		TaskId = taskId;
	}
}
