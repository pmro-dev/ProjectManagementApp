using App.Features.Projects.Show.ProjectManagerBoards.TeamsBoard;
using MediatR;
using System.Linq.Expressions;

namespace App.Features.Projects.Show;

public class ShowProjectTeamsBoardQuery : IRequest<ShowProjectTeamsBoardQueryResponse>
{
	public Guid ProjectId { get; set; }
	public int PageNumber { get; }
	public int ItemsPerPageCount { get; }
	public Expression<Func<TeamsBoardTeamVM, object>> OrderSelector { get; set; }

	public ShowProjectTeamsBoardQuery(Guid projectId, Expression<Func<TeamsBoardTeamVM, object>> orderSelector, int pageNumber, int itemsPerPageCount)
	{
		ProjectId = projectId;
		PageNumber = pageNumber;
		ItemsPerPageCount = itemsPerPageCount;
		OrderSelector = orderSelector;
	}
}

public record ShowProjectTeamsBoardQueryResponse(
	TeamsBoardOutputVM? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}