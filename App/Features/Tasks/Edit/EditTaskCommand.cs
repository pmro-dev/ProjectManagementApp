using App.Features.Tasks.Edit.Models;
using MediatR;

namespace App.Features.Tasks.Edit;

public class EditTaskCommand : IRequest<object>
{
	public TaskEditInputVM InputVM { get; }

	public EditTaskCommand(TaskEditInputVM taskEditInputVM)
	{
		InputVM = taskEditInputVM;
	}
}
