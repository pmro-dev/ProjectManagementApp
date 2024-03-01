using MediatR;

namespace App.Features.Teams.Delete;

public class DeleteTeamWithinProjectCommand : IRequest<DeleteTeamWithinProjectCommandResponse>
{
    public Guid TeamId { get; set; }

    public DeleteTeamWithinProjectCommand(Guid teamId)
    {
        TeamId = teamId;
    }
}

public record DeleteTeamWithinProjectCommandResponse(
    string? ErrorMessage = null,
    int StatusCode = StatusCodes.Status200OK
){}
