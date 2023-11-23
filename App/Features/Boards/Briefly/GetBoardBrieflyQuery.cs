using App.Features.Boards.Briefly.Models;
using MediatR;

namespace App.Features.Boards.Briefly;

public class GetBoardBrieflyQuery : IRequest<GetBoardBrieflyQueryResponse>
{
}

public record GetBoardBrieflyQueryResponse(BoardBrieflyOutputVM? Data, string? ErrorMessage = null, int StatusCode = StatusCodes.Status200OK) { }