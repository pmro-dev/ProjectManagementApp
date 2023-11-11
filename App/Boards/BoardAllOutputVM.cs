using Web.Boards.Interfaces;
using Web.TodoLists.Common.Interfaces;

namespace Web.Boards;

public class BoardAllOutputVM : IBoardAllOutputVM
{
	public ICollection<ITodoListDto> TodoLists { get; set; } = new List<ITodoListDto>();
}
