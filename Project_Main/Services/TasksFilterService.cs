using Project_DomainEntities.Helpers;
using Project_Main.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Project_Main.Services
{
    public static class TasksFilterService
	{
		private const int DateCompareValueEarlier = 0;

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
		private static DateTime todayDate = DateTime.Today;

		private static IEnumerable<ITaskDto> ExecuteFilter(IEnumerable<ITaskDto> source, Func<ITaskDto, bool> predicate)
        {
			return source.AsParallel().Where(predicate).ToList();
		}

		public static IEnumerable<ITaskDto> FilterForTasksForToday(IEnumerable<ITaskDto> tasks)
		{
			todayDate = DateTime.Today;

			Func<ITaskDto, bool> tasksForTodayPredicate = task =>
			task.DueDate.ToShortDateString() == todayDate.ToShortDateString() &&
			task.Status != TaskStatusHelper.TaskStatusType.Completed;

			return ExecuteFilter(tasks, tasksForTodayPredicate);
		}

		public static IEnumerable<ITaskDto> FilterForTasksCompleted(IEnumerable<ITaskDto> tasks)
		{
			todayDate = DateTime.Today;

			Func<ITaskDto, bool> tasksCompletedPredicate = task =>
			task.Status == TaskStatusHelper.TaskStatusType.Completed &&
			task.DueDate.CompareTo(todayDate) > DateCompareValueEarlier;

			return ExecuteFilter(tasks, tasksCompletedPredicate);
		}

		public static IEnumerable<ITaskDto> FilterForTasksNotCompleted(IEnumerable<ITaskDto> tasks, DateTime? filterDueDate)
		{
			todayDate = DateTime.Today;

			Func<ITaskDto, bool> tasksNotCompletedPredicate = task =>
			{
				if (filterDueDate is null)
				{
					return task.Status != TaskStatusHelper.TaskStatusType.Completed && task.DueDate.CompareTo(todayDate) > DateCompareValueEarlier;
				}

				return (task.Status != TaskStatusHelper.TaskStatusType.Completed) && (task.DueDate.CompareTo(filterDueDate) < DateCompareValueEarlier && task.DueDate.CompareTo(todayDate) > DateCompareValueEarlier);
			};

			return ExecuteFilter(tasks, tasksNotCompletedPredicate);
		}

		public static IEnumerable<ITaskDto> FilterForTasksExpired(IEnumerable<ITaskDto> tasks)
		{
			todayDate = DateTime.Today;

			Func<ITaskDto, bool> TasksExpiredPredicate = t =>
			t.DueDate.CompareTo(todayDate) < DateCompareValueEarlier;

			return ExecuteFilter(tasks, TasksExpiredPredicate);
		}
	}
}
