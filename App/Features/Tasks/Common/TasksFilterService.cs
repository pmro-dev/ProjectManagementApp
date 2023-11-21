using App.Common.Helpers;
using App.Features.Tasks.Common.Helpers;
using App.Features.Tasks.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Tasks.Common;

public static class TasksFilterService
{
	private const int DateCompareValueEarlier = 0;

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	private static DateTime todayDate = DateTime.Today;

	private static IEnumerable<TaskDto> ExecuteFilter(IEnumerable<TaskDto> source, Func<TaskDto, bool> predicate)
	{
		return source.AsParallel().Where(predicate).ToList();
	}

	public static IEnumerable<TaskDto> FilterForTasksForToday(IEnumerable<TaskDto> tasks)
	{
		todayDate = DateTime.Today;

		static bool tasksForTodayPredicate(TaskDto task) =>
		task.DueDate.ToShortDateString() == todayDate.ToShortDateString() &&
		task.Status != TaskStatusHelper.TaskStatusType.Completed;

		return ExecuteFilter(tasks, tasksForTodayPredicate);
	}

	public static IEnumerable<TaskDto> FilterForTasksCompleted(IEnumerable<TaskDto> tasks)
	{
		todayDate = DateTime.Today;

		static bool tasksCompletedPredicate(TaskDto task) =>
		task.Status == TaskStatusHelper.TaskStatusType.Completed &&
		task.DueDate.CompareTo(todayDate) > DateCompareValueEarlier;

		return ExecuteFilter(tasks, tasksCompletedPredicate);
	}

	public static IEnumerable<TaskDto> FilterForTasksNotCompleted(IEnumerable<TaskDto> tasks, DateTime? filterDueDate)
	{
		todayDate = DateTime.Today;

		bool tasksNotCompletedPredicate(TaskDto task)
		{
			if (filterDueDate is null)
			{
				return task.Status != TaskStatusHelper.TaskStatusType.Completed && task.DueDate.CompareTo(todayDate) > DateCompareValueEarlier;
			}

			return task.Status != TaskStatusHelper.TaskStatusType.Completed && task.DueDate.CompareTo(filterDueDate) < DateCompareValueEarlier && task.DueDate.CompareTo(todayDate) > DateCompareValueEarlier;
		}

		return ExecuteFilter(tasks, tasksNotCompletedPredicate);
	}

	public static IEnumerable<TaskDto> FilterForTasksExpired(IEnumerable<TaskDto> tasks)
	{
		todayDate = DateTime.Today;

		static bool TasksExpiredPredicate(TaskDto t) =>
		t.DueDate.CompareTo(todayDate) < DateCompareValueEarlier;

		return ExecuteFilter(tasks, TasksExpiredPredicate);
	}
}
