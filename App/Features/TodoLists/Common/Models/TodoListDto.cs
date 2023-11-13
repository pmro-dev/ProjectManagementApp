using App.Features.Tasks.Common.Interfaces;
using App.Features.TodoLists.Common.Interfaces;

namespace App.Features.TodoLists.Common.Models;

public class TodoListDto : ITodoListDto
{
	public int Id { get; set; }

	public string Title { get; set; } = string.Empty;

	public string UserId { get; set; } = string.Empty;

	public ICollection<ITaskDto> Tasks { get; set; } = new List<ITaskDto>();
}
