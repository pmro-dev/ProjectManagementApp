using App.Features.TodoLists.Common.Interfaces;

namespace App.Features.Boards.Interfaces;

public interface IBoardViewModelsFactory
{
	public IBoardBrieflyOutputVM CreateBrieflyOutputVM(ICollection<ITodoListDto> todolistDto);
	public IBoardAllOutputVM CreateAllOutputVM(ICollection<ITodoListDto> todolistDto);
}