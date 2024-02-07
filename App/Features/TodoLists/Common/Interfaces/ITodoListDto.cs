using App.Features.Tasks.Common.Models;

namespace App.Features.TodoLists.Common.Interfaces;

public interface ITodoListDto
{
	Guid Id { get; set; }
	ICollection<TaskDto> Tasks { get; set; }
	string Title { get; set; }
	string UserId { get; set; }
}