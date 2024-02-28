using MediatR;

namespace App.Features.Teams.Common.Delete;

public class DeleteTeamSchemeCommand : IRequest<DeleteTeamSchemeCommandResponse>
{
	public Guid TeamId { get; set; }

	public DeleteTeamSchemeCommand(Guid teamId)
	{
		TeamId = teamId;
	}
}

public record DeleteTeamSchemeCommandResponse(
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}
