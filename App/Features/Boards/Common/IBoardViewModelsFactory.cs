using App.Features.Boards.ShowAll;
using App.Features.Boards.ShowBriefly;
using App.Features.TodoLists.Common.Models;

namespace App.Features.Boards.Common;

public interface IBoardViewModelsFactory
{
    public BoardBrieflyOutputVM CreateBrieflyOutputVM(ICollection<TodoListDto> todolistDto);
    public BoardAllOutputVM CreateAllOutputVM(ICollection<TodoListDto> todolistDto);
}