using App.Features.Boards.All.Models;
using App.Features.Tasks.Common.Models;
using App.Features.TodoLists.Common.Models;
using MediatR;
using System.Linq.Expressions;

namespace App.Features.Boards.All;

public class GetBoardAllQuery : IRequest<GetBoardAllQueryResponse>
{
	public int PageNumber { get; }
	public int ItemsPerPageCount { get; }
	public Expression<Func<TodoListModel, object>> OrderBySelector { get; }
	public Expression<Func<TaskModel, object>> OrderDetailsBySelector { get; }

	public GetBoardAllQuery(int pageNumber, int itemsPerPageCount, Expression<Func<TodoListModel, object>> orderBySelector,
		Expression<Func<TaskModel, object>> orderDetailsBySelector)
	{
		PageNumber = pageNumber;
		ItemsPerPageCount = itemsPerPageCount;
		OrderBySelector = orderBySelector;
		OrderDetailsBySelector = orderDetailsBySelector;
	}
}

public record GetBoardAllQueryResponse(
	BoardAllOutputVM? Data, 
	string? ErrorMessage = null, 
	int StatusCode = StatusCodes.Status200OK)
{}