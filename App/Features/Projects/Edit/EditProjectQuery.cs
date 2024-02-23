using App.Common.ViewModels;
using App.Features.Projects.Edit.Models;
using MediatR;

namespace App.Features.Projects.Edit;

public class EditProjectQuery : IRequest<EditProjectQueryResponse>
{
	public Guid ProjectId {  get; set; }

	public EditProjectQuery(Guid projectId)
	{
		ProjectId = projectId;
	}
}

public record EditProjectQueryResponse(
	WrapperViewModel<ProjectEditInputVM, ProjectEditOutputVM>? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}