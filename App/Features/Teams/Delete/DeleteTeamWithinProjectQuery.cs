using MediatR;

namespace App.Features.Teams.Delete;

public class DeleteTeamWithinProjectQuery : IRequest<DeleteTeamWithinProjectQueryResponse>
{
    public Guid TeamId { get; set; }

    public DeleteTeamWithinProjectQuery(Guid teamId)
    {
        TeamId = teamId;
    }
}

public record DeleteTeamWithinProjectQueryResponse(
    DeleteTeamOutputVM? Data,
    string? ErrorMessage = null,
    int StatusCode = StatusCodes.Status200OK
)
{ }
