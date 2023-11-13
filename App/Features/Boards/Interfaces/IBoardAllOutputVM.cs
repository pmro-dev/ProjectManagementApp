using App.Features.TodoLists.Common.Interfaces;

namespace App.Features.Boards.Interfaces;

public interface IBoardAllOutputVM
{
	ICollection<ITodoListDto> TodoLists { get; set; }
}