using MediatR;

namespace App.Features.Teams.Show;

public class ShowTeamWithinProjectQuery : IRequest<ShowTeamWithinProjectQueryResponse>
{
    public Guid TeamId { get; set; }

	public ShowTeamWithinProjectQuery(Guid teamId)
	{
		TeamId = teamId;
	}
}

public record ShowTeamWithinProjectQueryResponse(
    TeamWithinProjectOutputVM? Data,
    string? ErrorMessage = null,
    int StatusCode = StatusCodes.Status200OK
){}
