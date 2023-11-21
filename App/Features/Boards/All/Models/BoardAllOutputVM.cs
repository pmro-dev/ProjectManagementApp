using App.Features.Boards.All.Interfaces;
using App.Features.TodoLists.Common.Models;

namespace App.Features.Boards.All.Models;

public class BoardAllOutputVM : IBoardAllOutputVM
{
    public ICollection<TodoListDto> TodoLists { get; set; } = new List<TodoListDto>();
}
