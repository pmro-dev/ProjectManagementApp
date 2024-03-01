using MediatR;

namespace App.Features.Teams.Edit;

public class EditTeamSchemeCommand : IRequest<EditTeamSchemeCommandResponse>
{
    public Guid TeamId { get; }
    public EditTeamInputVM InputVM { get; }

    public EditTeamSchemeCommand(Guid teamId, EditTeamInputVM inputVM)
    {
        TeamId = teamId;
        InputVM = inputVM;
    }
}

public record EditTeamSchemeCommandResponse(
    string? ErrorMessage = null,
    int StatusCode = StatusCodes.Status201Created
)
{ }
