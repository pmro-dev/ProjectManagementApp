using App.Common.ViewModels;
using MediatR;

namespace App.Features.Teams.Common.Edit;

public class EditTeamWithinProjectScopeQuery : IRequest<EditTeamWithinProjectScopeResponse>
{
	public Guid TeamId { get; }
	public Guid ProjectId { get; }

	public EditTeamWithinProjectScopeQuery(Guid projectId,Guid teamId)
	{
		ProjectId = projectId;
		TeamId = teamId;
	}
}

public record EditTeamWithinProjectScopeResponse(
	WrapperViewModel<EditTeamInputVM, EditTeamOutputVM>? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}
