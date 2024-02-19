using App.Features.Tags.Common.Models;
using App.Features.TodoLists.Common.Models;

namespace App.Features.TodoLists.Common.Tags;

public class TodoListTagDto : ITodoListTagDto
{
	public byte[] RowVersion { get; set; } = { 1, 1, 1 };

	public Guid TodoListId { get; set; } = Guid.Empty;

	public TodoListDto? TodoList { get; set; }

	public Guid TagId { get; set; } = Guid.Empty;

	public TagDto? Tag { get; set; }
}
