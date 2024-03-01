using MediatR;

namespace App.Features.Teams.Create;

public class CreateTeamWithinProjectCommand : IRequest<CreateTeamWithinProjectCommandResponse>
{
    public CreateTeamInputVM InputVM { get; }

    public CreateTeamWithinProjectCommand(CreateTeamInputVM inputVM)
    {
        InputVM = inputVM;
    }
}

public record CreateTeamWithinProjectCommandResponse(
    string? ErrorMessage = null,
    int StatusCode = StatusCodes.Status201Created
)
{ }
