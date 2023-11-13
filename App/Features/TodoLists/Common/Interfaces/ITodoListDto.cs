using App.Features.Tasks.Common.Interfaces;

namespace App.Features.TodoLists.Common.Interfaces;

public interface ITodoListDto
{
	int Id { get; set; }
	ICollection<ITaskDto> Tasks { get; set; }
	string Title { get; set; }
	string UserId { get; set; }
}