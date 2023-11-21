using App.Features.Boards.Briefly.Interfaces;
using MediatR;

namespace App.Features.Boards.Briefly;

public class GetBoardBrieflyQuery : IRequest<IBoardBrieflyOutputVM>
{
}
