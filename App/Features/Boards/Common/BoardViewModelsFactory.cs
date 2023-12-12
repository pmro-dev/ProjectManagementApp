using App.Features.Boards.All.Models;
using App.Features.Boards.Briefly.Models;
using App.Features.Boards.Common.Interfaces;
using App.Features.Pagination;
using App.Features.TodoLists.Common.Models;

namespace App.Features.Boards.Common;

public class BoardViewModelsFactory : IBoardViewModelsFactory
{
	public BoardAllOutputVM CreateAllOutputVM(ICollection<TodoListDto> todolistDtos)
	{
		return new BoardAllOutputVM()
		{
			TodoLists = todolistDtos
		};
	}

	public BoardBrieflyOutputVM CreateBrieflyOutputVM(List<Tuple<TodoListDto, int, int>> tupleDtos, PaginationData paginationData)
	{
		return new BoardBrieflyOutputVM(tupleDtos, paginationData);
	}
}
