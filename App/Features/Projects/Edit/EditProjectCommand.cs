using App.Features.Projects.Edit.Models;
using MediatR;

namespace App.Features.Projects.Edit;

public class EditProjectCommand : IRequest<EditProjectCommandResponse>
{
    public ProjectEditInputVM InputVM { get; set; }
    public Guid ProjectId { get; set; }

	public EditProjectCommand(ProjectEditInputVM inputVM, Guid projectId)
	{
		InputVM = inputVM;
		ProjectId = projectId;
	}
}

public record EditProjectCommandResponse(
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status201Created
){}