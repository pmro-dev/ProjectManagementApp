using App.Features.Boards.All.Models;
using App.Features.Boards.Briefly.Models;
using App.Features.TodoLists.Common.Models;

namespace App.Features.Boards.Common.Interfaces;

public interface IBoardViewModelsFactory
{
    public BoardBrieflyOutputVM CreateBrieflyOutputVM(ICollection<TodoListDto> todolistDto);
    public BoardAllOutputVM CreateAllOutputVM(ICollection<TodoListDto> todolistDto);
}