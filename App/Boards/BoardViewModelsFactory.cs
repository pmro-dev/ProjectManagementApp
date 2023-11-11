using Web.Boards.Interfaces;
using Web.TodoLists.Common.Interfaces;

namespace Web.Boards;

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
