using App.Features.Tasks.Common.Models;

namespace App.Features.TodoLists.Show.Interfaces;

public interface ITodoListDetailsOutputVM
{
	int Id { get; set; }
	string Name { get; set; }
	IEnumerable<TaskDto> TasksCompleted { get; set; }
	IEnumerable<TaskDto> TasksExpired { get; set; }
	IEnumerable<TaskDto> TasksForToday { get; set; }
	IEnumerable<TaskDto> TasksNotCompleted { get; set; }
	string UserId { get; set; }
}