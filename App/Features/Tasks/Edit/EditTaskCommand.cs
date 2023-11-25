using App.Common;
using App.Features.Tasks.Edit.Models;
using MediatR;

namespace App.Features.Tasks.Edit;

public class EditTaskCommand : IRequest<EditTaskCommandResponse>
{
	public TaskEditInputVM InputVM { get; }

	public EditTaskCommand(TaskEditInputVM taskEditInputVM)
	{
		InputVM = taskEditInputVM;
	}
}

public record EditTaskCommandResponse(CustomRouteValues? Data = null, string? ErrorMessage = null, int StatusCode = StatusCodes.Status201Created) { }
