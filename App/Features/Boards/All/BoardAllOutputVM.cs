using App.Features.Boards.Interfaces;
using App.Features.TodoLists.Common.Interfaces;

namespace App.Features.Boards.ShowAll;

public class BoardAllOutputVM : IBoardAllOutputVM
{
	public ICollection<ITodoListDto> TodoLists { get; set; } = new List<ITodoListDto>();
}
