using MediatR;

namespace App.Features.Teams.Common.Delete;

public class DeleteTeamSchemeQuery : IRequest<DeleteTeamSchemeQueryResponse>
{
	public Guid TeamId { get; set; }

	public DeleteTeamSchemeQuery(Guid teamId)
	{
		TeamId = teamId;
	}
}

public record DeleteTeamSchemeQueryResponse(
	DeleteTeamOutputVM? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}
