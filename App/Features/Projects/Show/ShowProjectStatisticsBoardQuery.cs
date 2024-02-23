using App.Features.Projects.Show.ProjectManagerBoards.ProjectBoard;
using App.Features.Projects.Show.ProjectManagerBoards.StatisticsBoard;
using MediatR;
using System.Linq.Expressions;

namespace App.Features.Projects.Show;

public class ShowProjectStatisticsBoardQuery : IRequest<ShowProjectStatisticsBoardQueryResponse>
{
	public Guid ProjectId { get; }
	public int PageNumber { get; }
	public int ItemsPerPageCount { get; }

	// Here is specified selector for sorting to do lists, in the final version, user should choose: sort by the name, best or worst progress... 
	public Expression<Func<StatisticsBoardTodoListVM, object>> OrderTodoListsBySelector { get; }

	// Here is specified selector for sorting teams, in the final version, user should choose: sort by the name, best or worst progress... 
	public Expression<Func<StatisticsBoardTeamVM, object>> OrderTeamsBySelector { get; }

	public ShowProjectStatisticsBoardQuery(Guid projectId,
		Expression<Func<StatisticsBoardTodoListVM, object>> orderTodoListsBySelector,
		Expression<Func<StatisticsBoardTeamVM, object>> orderTeamsBySelector,
		int pageNumber, int itemsPerPageCount)
	{
		ProjectId = projectId;
		OrderTodoListsBySelector = orderTodoListsBySelector;
		OrderTeamsBySelector = orderTeamsBySelector;
		PageNumber = pageNumber;
		ItemsPerPageCount = itemsPerPageCount;
	}
}

public record ShowProjectStatisticsBoardQueryResponse(
	ProjectBoardStatisticsOutputVM? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}