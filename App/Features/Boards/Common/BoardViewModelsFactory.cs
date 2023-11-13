using App.Features.Boards.Interfaces;
using App.Features.Boards.ShowAll;
using App.Features.Boards.ShowBriefly;
using App.Features.TodoLists.Common.Interfaces;

namespace App.Features.Boards.Common;

public class BoardViewModelsFactory : IBoardViewModelsFactory
{
	public IBoardAllOutputVM CreateAllOutputVM(ICollection<ITodoListDto> todolistDto)
	{
		return new BoardAllOutputVM()
		{
			TodoLists = todolistDto
		};
	}

	public IBoardBrieflyOutputVM CreateBrieflyOutputVM(ICollection<ITodoListDto> todolistDto)
	{
		return new BoardBrieflyOutputVM()
		{
			TodoLists = todolistDto
		};
	}
}
