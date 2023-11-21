using App.Features.Tasks.Common.Models;
using App.Features.TodoLists.Show.Interfaces;

namespace App.Features.TodoLists.Show;

/// <summary>
/// Model for showing ToDoList.
/// </summary>
public class TodoListDetailsOutputVM : ITodoListDetailsOutputVM
{
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public string UserId { get; set; } = string.Empty;

	public IEnumerable<TaskDto> TasksForToday { get; set; } = new List<TaskDto>();
	public IEnumerable<TaskDto> TasksCompleted { get; set; } = new List<TaskDto>();
	public IEnumerable<TaskDto> TasksNotCompleted { get; set; } = new List<TaskDto>();
	public IEnumerable<TaskDto> TasksExpired { get; set; } = new List<TaskDto>();
}
