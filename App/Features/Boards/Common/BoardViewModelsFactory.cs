using App.Features.Boards.ShowAll;
using App.Features.Boards.ShowBriefly;
using App.Features.TodoLists.Common.Models;

namespace App.Features.Boards.Common;

public class BoardViewModelsFactory : IBoardViewModelsFactory
{
	public BoardAllOutputVM CreateAllOutputVM(ICollection<TodoListDto> todolistDto)
	{
		return new BoardAllOutputVM()
		{
			TodoLists = todolistDto
		};
	}

	public BoardBrieflyOutputVM CreateBrieflyOutputVM(ICollection<TodoListDto> todolistDto)
	{
		return new BoardBrieflyOutputVM()
		{
			TodoLists = todolistDto
		};
	}
}
