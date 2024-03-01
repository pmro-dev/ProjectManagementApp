using MediatR;

namespace App.Features.Teams.Show;

public class ShowTeamSchemeQuery : IRequest<ShowTeamSchemeQueryResponse>
{
    public Guid TeamId { get; set; }

	public ShowTeamSchemeQuery(Guid teamId)
	{
		TeamId = teamId;
	}
}

public record ShowTeamSchemeQueryResponse(
    TeamSchemeOutputVM? Data,
    string? ErrorMessage = null,
    int StatusCode = StatusCodes.Status200OK
){}
