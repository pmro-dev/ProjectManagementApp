using Web.TodoLists.Common.Interfaces;

namespace Web.Boards.Interfaces;

public interface IBoardViewModelsFactory
{
    public IBoardBrieflyOutputVM CreateBrieflyOutputVM(ICollection<ITodoListDto> todolistDto);
    public IBoardAllOutputVM CreateAllOutputVM(ICollection<ITodoListDto> todolistDto);
}