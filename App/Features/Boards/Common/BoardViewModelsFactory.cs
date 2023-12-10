﻿using App.Features.Boards.All.Models;
using App.Features.Boards.Briefly.Models;
using App.Features.Boards.Common.Interfaces;
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

	public BoardBrieflyOutputVM CreateBrieflyOutputVM(List<Tuple<TodoListDto, int, int>> tupletDtosList)
	{
		return new BoardBrieflyOutputVM()
		{
			TupleDtos = tupletDtosList
		};
	}
}
