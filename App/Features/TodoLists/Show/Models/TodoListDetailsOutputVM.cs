using App.Features.Pagination;
using App.Features.Tasks.Common.Models;
using App.Features.TodoLists.Show.Interfaces;

namespace App.Features.TodoLists.Show.Models;

public class TodoListDetailsOutputVM : ITodoListDetailsOutputVM
{
    public int Id { get; }
    public string Name { get; }
    public string UserId { get; }
    public PaginationData PaginData { get; }
    public IEnumerable<TaskDto> TasksForToday { get; }
    public IEnumerable<TaskDto> TasksCompleted { get;}
    public IEnumerable<TaskDto> TasksNotCompleted { get; }
    public IEnumerable<TaskDto> TasksExpired { get; }
	public int TasksForTodayCount { get; }
	public int TasksCompletedCount { get; }
	public int TasksNotCompletedCount { get; }
	public int TasksExpiredCount { get; }

	public TodoListDetailsOutputVM(
		int id, string name, string userId, PaginationData paginData, 
		IEnumerable<TaskDto> tasksForToday, IEnumerable<TaskDto> tasksCompleted, 
		IEnumerable<TaskDto> tasksNotCompleted, IEnumerable<TaskDto> tasksExpired)
	{
		Id = id;
		Name = name;
		UserId = userId;
		PaginData = paginData;
		TasksForToday = tasksForToday;
		TasksCompleted = tasksCompleted;
		TasksNotCompleted = tasksNotCompleted;
		TasksExpired = tasksExpired;

		TasksForTodayCount = TasksForToday.Count();
		TasksCompletedCount = TasksCompleted.Count();
		TasksNotCompletedCount = TasksNotCompleted.Count();
		TasksExpiredCount = TasksExpired.Count();
	}
}