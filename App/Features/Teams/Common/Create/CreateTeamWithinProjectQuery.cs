using App.Common.ViewModels;
using MediatR;

namespace App.Features.Teams.Common.Create;

public class CreateTeamWithinProjectQuery : IRequest<CreateTeamWithinProjectQueryResponse>
{
	public Guid ProjectId { get; }

	public CreateTeamWithinProjectQuery(Guid projectId)
	{
		ProjectId = projectId;
	}
}


public record CreateTeamWithinProjectQueryResponse(
	WrapperViewModel<CreateTeamInputVM, CreateTeamOutputVM>? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}