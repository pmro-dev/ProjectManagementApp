using App.Features.Pagination;
using App.Features.Tasks.Common.Models;

namespace App.Features.TodoLists.Show.Interfaces;

public interface ITodoListDetailsOutputVM
{
	Guid Id { get; }
	string Name { get; }
	string UserId { get; }
	public PaginationData PaginData { get; }
	IEnumerable<TaskDto> TasksCompleted { get; }
	IEnumerable<TaskDto> TasksExpired { get; }
	IEnumerable<TaskDto> TasksForToday { get; }
	IEnumerable<TaskDto> TasksNotCompleted { get; }
}