using App.Features.Boards.Briefly.Models;
using App.Features.TodoLists.Common.Models;
using MediatR;
using System.Linq.Expressions;

namespace App.Features.Boards.Briefly;

public class GetBoardBrieflyQuery : IRequest<GetBoardBrieflyQueryResponse>
{
	public int PageNumber { get; }
	public int ItemsPerPageCount { get; }
	public Expression<Func<TodoListModel, object>> OrderBySelector { get; }

	public GetBoardBrieflyQuery(int pageNumber, int itemsPerPageCount, Expression<Func<TodoListModel, object>> orderBySelector)
	{
		PageNumber = pageNumber;
		ItemsPerPageCount = itemsPerPageCount;
		OrderBySelector = orderBySelector;
	}
}

public record GetBoardBrieflyQueryResponse(
	BoardBrieflyOutputVM? Data, 
	string? ErrorMessage = null, 
	int StatusCode = StatusCodes.Status200OK)
{}