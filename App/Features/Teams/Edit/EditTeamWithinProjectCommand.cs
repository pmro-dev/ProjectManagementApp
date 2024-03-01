using MediatR;

namespace App.Features.Teams.Edit;

public class EditTeamWithinProjectCommand : IRequest<EditTeamWithinProjectCommandResponse>
{
    public Guid ProjectId { get; }
    public Guid TeamId { get; }
    public EditTeamInputVM InputVM { get; }

    public EditTeamWithinProjectCommand(Guid projectId, Guid teamId, EditTeamInputVM inputVM)
    {
        InputVM = inputVM;
        ProjectId = projectId;
        TeamId = teamId;
    }
}

public record EditTeamWithinProjectCommandResponse(
    string? ErrorMessage = null,
    int StatusCode = StatusCodes.Status201Created
)
{ }