using App.Features.Tasks.Common.Models;
using App.Features.TodoLists.Common.Interfaces;

namespace App.Features.TodoLists.Common.Models;

public class TodoListDto : ITodoListDto
{
	public int Id { get; set; }

	public string Title { get; set; } = string.Empty;

	public string UserId { get; set; } = string.Empty;

	public ICollection<TaskDto> Tasks { get; set; } = new List<TaskDto>();
}
