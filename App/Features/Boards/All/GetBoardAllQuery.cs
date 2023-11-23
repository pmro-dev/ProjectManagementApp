using App.Features.Boards.All.Models;
using MediatR;

namespace App.Features.Boards.All;

public class GetBoardAllQuery : IRequest<GetBoardAllQueryResponse>
{
}
public record GetBoardAllQueryResponse(BoardAllOutputVM? Data, string? ErrorMessage = null, int StatusCode = StatusCodes.Status200OK) { }