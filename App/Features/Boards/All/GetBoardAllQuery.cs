using App.Features.Boards.All.Interfaces;
using MediatR;

namespace App.Features.Boards.All;

public class GetBoardAllQuery : IRequest<IBoardAllOutputVM>
{
}
