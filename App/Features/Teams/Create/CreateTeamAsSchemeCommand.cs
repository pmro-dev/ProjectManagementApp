using MediatR;

namespace App.Features.Teams.Create;

public class CreateTeamAsSchemeCommand : IRequest<CreateTeamAsSchemeCommandResponse>
{
    public CreateTeamInputVM InputVM { get; }

    public CreateTeamAsSchemeCommand(CreateTeamInputVM inputVM)
    {
        InputVM = inputVM;
    }
}

public record CreateTeamAsSchemeCommandResponse(
    string? ErrorMessage = null,
    int StatusCode = StatusCodes.Status201Created
)
{ }