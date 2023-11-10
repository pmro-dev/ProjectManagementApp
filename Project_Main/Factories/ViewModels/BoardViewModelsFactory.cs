using Application.DTOs.Entities;
using Web.ViewModels.Outputs;
using Web.ViewModels.Outputs.Abstract;

namespace Web.Factories.ViewModels;

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
