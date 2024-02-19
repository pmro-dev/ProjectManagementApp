using App.Features.Tags.Common.Models;
using App.Features.TodoLists.Common.Models;

namespace App.Features.TodoLists.Common.Tags;

public interface ITodoListTagDto
{
	byte[] RowVersion { get; set; }

	Guid TodoListId { get; set; }

	TodoListDto? TodoList { get; set; }

	Guid TagId { get; set; }

	TagDto? Tag { get; set; }
}
