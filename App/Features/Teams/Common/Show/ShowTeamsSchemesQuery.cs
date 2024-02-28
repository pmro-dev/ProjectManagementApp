using App.Features.Tasks.Common.Models;
using MediatR;
using System.Linq.Expressions;

namespace App.Features.Teams.Common.Show;

public class ShowTeamsSchemesQuery : IRequest<ShowTeamsSchemesQueryResponse>
{
	public int PageNumber { get; }
	public int ItemsPerPageCount { get; }
	public Expression<Func<TaskModel, object>> OrderDetailsBySelector { get; }

	public ShowTeamsSchemesQuery(int pageNumber, int itemsPerPageCount, Expression<Func<TaskModel, object>> orderDetailsBySelector)
	{
		PageNumber = pageNumber;
		ItemsPerPageCount = itemsPerPageCount;
		OrderDetailsBySelector = orderDetailsBySelector;
	}
}

public record ShowTeamsSchemesQueryResponse(
	TeamsSchemesOutputVM? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}
