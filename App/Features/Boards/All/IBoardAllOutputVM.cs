using App.Features.TodoLists.Common.Models;

namespace App.Features.Boards.All;

public interface IBoardAllOutputVM
{
    ICollection<TodoListDto> TodoLists { get; set; }
}