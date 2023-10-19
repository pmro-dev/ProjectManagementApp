using Project_DomainEntities;
using Project_DomainEntities.Helpers;

namespace Project_Main.Services
{
	public static class TasksFilterService
	{
		private const int DateCompareValueEarlier = 0;
		private static DateTime todayDate = DateTime.Today;

		private static readonly Func<TaskModel, TaskModel> createNewTaskModelSelector = task => new TaskModel
		{
			Id = task.Id,
			Title = task.Title,
			Description = task.Description,
			DueDate = task.DueDate,
			CreationDate = task.CreationDate,
			LastModificationDate = task.LastModificationDate,
			ReminderDate = task.ReminderDate,
			Status = task.Status,
			TaskTags = task.TaskTags,
			TodoListId = task.TodoListId,
			TodoList = task.TodoList,
			UserId = task.UserId,
		};

		private static List<TaskModel> ExecuteFilter(IEnumerable<TaskModel> source, Func<TaskModel, bool> predicate, Func<TaskModel, TaskModel> selector)
		{
			return source.Where(predicate).Select(selector).ToList();
		}

		public static List<TaskModel> FilterForTasksForToday(IEnumerable<TaskModel> tasks)
		{
			todayDate = DateTime.Today;

			Func<TaskModel, bool> tasksForTodayPredicate = task =>
			task.DueDate.ToShortDateString() == todayDate.ToShortDateString() &&
			task.Status != TaskStatusHelper.TaskStatusType.Completed;

			return ExecuteFilter(tasks, tasksForTodayPredicate, createNewTaskModelSelector);
		}

		public static List<TaskModel> FilterForTasksCompleted(IEnumerable<TaskModel> tasks)
		{
			todayDate = DateTime.Today;

			Func<TaskModel, bool> tasksCompletedPredicate = task =>
			task.Status == TaskStatusHelper.TaskStatusType.Completed &&
			task.DueDate.CompareTo(todayDate) > DateCompareValueEarlier;

			return ExecuteFilter(tasks, tasksCompletedPredicate, createNewTaskModelSelector);
		}

		public static List<TaskModel> FilterForTasksNotCompleted(IEnumerable<TaskModel> tasks, DateTime? filterDueDate)
		{
			todayDate = DateTime.Today;

			Func<TaskModel, bool> tasksNotCompletedPredicate = task =>
			{
				if (filterDueDate is null)
				{
					return task.Status != TaskStatusHelper.TaskStatusType.Completed && task.DueDate.CompareTo(todayDate) > DateCompareValueEarlier;
				}

				return (task.Status != TaskStatusHelper.TaskStatusType.Completed) && (task.DueDate.CompareTo(filterDueDate) < DateCompareValueEarlier && task.DueDate.CompareTo(todayDate) > DateCompareValueEarlier);
			};

			return ExecuteFilter(tasks, tasksNotCompletedPredicate, createNewTaskModelSelector);
		}

		public static List<TaskModel> FilterForTasksExpired(IEnumerable<TaskModel> tasks)
		{
			todayDate = DateTime.Today;

			Func<TaskModel, bool> TasksExpiredPredicate = t =>
			t.DueDate.CompareTo(todayDate) < DateCompareValueEarlier;

			return ExecuteFilter(tasks, TasksExpiredPredicate, createNewTaskModelSelector);
		}
	}
}
