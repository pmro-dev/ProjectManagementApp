using App.Common.Helpers;
using App.Features.Tasks.Common.Models;
using System.ComponentModel.DataAnnotations;
using static App.Features.Tasks.Common.Helpers.TaskStatusHelper;

namespace App.Features.Tasks.Common;

public static class TasksFilterService
{
	private const int EarlierDateIndicator = 0;

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	private static DateTime todayDate = DateTime.Today;

	private static readonly Func<TaskDto, object> dueDayOrderSelector = task => task.DueDate;
	private static bool NotOverDueTaskSelector(TaskDto task) => task.DueDate.CompareTo(todayDate) > EarlierDateIndicator;
	private static bool OverDueTaskSelector(TaskDto task) => task.DueDate.CompareTo(todayDate) < EarlierDateIndicator;
	private static bool TodayTaskSelector(TaskDto task) => task.DueDate.ToShortDateString() == todayDate.ToShortDateString();
	private static bool OverDueTaskWithFilterSelector(TaskDto task, DateTime? filterDueDate) => task.DueDate.CompareTo(filterDueDate) < EarlierDateIndicator;
	private static bool TasksCompletedSelector(TaskDto task) => task.Status == TaskStatusType.Completed;
	private static bool TaskNotCompletedSelector(TaskDto task) => task.Status != TaskStatusType.Completed;

	private static IEnumerable<TaskDto> ExecuteFilter(IEnumerable<TaskDto> source, Func<TaskDto, bool> filter, Func<TaskDto, object> orderSelector)
	{
		return source.Where(filter).OrderBy(orderSelector).ToList();
	}

	public static IEnumerable<TaskDto> FilterForTasksForToday(IEnumerable<TaskDto> tasks)
	{
		todayDate = DateTime.Today;

		static bool todayNotCompletedTaskFilter(TaskDto task) => TodayTaskSelector(task) && TaskNotCompletedSelector(task);

		return ExecuteFilter(tasks, todayNotCompletedTaskFilter, dueDayOrderSelector);
	}

	public static IEnumerable<TaskDto> FilterForTasksCompleted(IEnumerable<TaskDto> tasks, DateTime? filterDueDate)
	{
		todayDate = DateTime.Today;

		bool compoundFilter(TaskDto task) => TasksCompletedSelector(task) && NotOverDueTaskSelector(task);

		return ExecuteFilter(tasks, compoundFilter, dueDayOrderSelector);
	}

	public static IEnumerable<TaskDto> FilterForTasksNotCompleted(IEnumerable<TaskDto> tasks, DateTime? filterDueDate)
	{
		todayDate = DateTime.Today;

		bool compoundFilter(TaskDto task)
		{
			if (filterDueDate is null)
				return
					TaskNotCompletedSelector(task) &&
					NotOverDueTaskSelector(task);
			else
				return
					TaskNotCompletedSelector(task) &&
					OverDueTaskWithFilterSelector(task, filterDueDate) &&
					NotOverDueTaskSelector(task);
		}

		return ExecuteFilter(tasks, compoundFilter, dueDayOrderSelector);
	}

	public static IEnumerable<TaskDto> FilterForTasksExpired(IEnumerable<TaskDto> tasks, DateTime? filterDueDate)
	{
		todayDate = DateTime.Today;

		bool compoundFilter(TaskDto task)
		{
			if (filterDueDate is null)
				return
					OverDueTaskSelector(task);
			else
				return
					OverDueTaskWithFilterSelector(task, filterDueDate);
		}

		return ExecuteFilter(tasks, compoundFilter, dueDayOrderSelector);
	}
}
