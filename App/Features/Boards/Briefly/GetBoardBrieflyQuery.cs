using App.Features.Boards.Briefly.Models;
using MediatR;

namespace App.Features.Boards.Briefly;

public class GetBoardBrieflyQuery : IRequest<GetBoardBrieflyQueryResponse>
{
	public int PageNumber { get; }
	public int ItemsPerPageCount { get; }

	public GetBoardBrieflyQuery(int pageNumber, int itemsPerPageCount)
	{
		PageNumber = pageNumber;
		ItemsPerPageCount = itemsPerPageCount;
	}
}

public record GetBoardBrieflyQueryResponse(BoardBrieflyOutputVM? Data, string? ErrorMessage = null, int StatusCode = StatusCodes.Status200OK) { }