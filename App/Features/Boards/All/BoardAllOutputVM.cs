using App.Features.Boards.All;
using App.Features.TodoLists.Common.Models;

namespace App.Features.Boards.ShowAll;

public class BoardAllOutputVM : IBoardAllOutputVM
{
	public ICollection<TodoListDto> TodoLists { get; set; } = new List<TodoListDto>();
}
