using MediatR;

namespace App.Features.Teams.Edit;

public class EditTeamWithinProjectScopeCommand : IRequest<EditTeamWithinProjectScopeCommandResponse>
{
    public Guid ProjectId { get; }
    public Guid TeamId { get; }
    public EditTeamInputVM InputVM { get; }

    public EditTeamWithinProjectScopeCommand(Guid projectId, Guid teamId, EditTeamInputVM inputVM)
    {
        InputVM = inputVM;
        ProjectId = projectId;
        TeamId = teamId;
    }
}

public record EditTeamWithinProjectScopeCommandResponse(
    string? ErrorMessage = null,
    int StatusCode = StatusCodes.Status201Created
)
{ }