using Project_DomainEntities;
using Project_DomainEntities.Helpers;
using Project_DTO;

namespace Project_Main.Services
{
	public static class TasksFilterService
	{
		private const int DateCompareValueEarlier = 0;
		private static DateTime todayDate = DateTime.Today;

		private static readonly Func<ITaskModel, ITaskModel> createNewTaskModelDtoSelector = task => new TaskModelDto
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

		private static IEnumerable<ITaskModel> ExecuteFilter(IEnumerable<ITaskModel> source, Func<ITaskModel, bool> predicate, Func<ITaskModel, ITaskModel> selector)
		{
			return source.AsParallel().Where(predicate).Select(selector).ToList();
		}

		public static IEnumerable<ITaskModel> FilterForTasksForToday(IEnumerable<ITaskModel> tasks)
		{
			todayDate = DateTime.Today;

			Func<ITaskModel, bool> tasksForTodayPredicate = task =>
			task.DueDate.ToShortDateString() == todayDate.ToShortDateString() &&
			task.Status != TaskStatusHelper.TaskStatusType.Completed;

			return ExecuteFilter(tasks, tasksForTodayPredicate, createNewTaskModelDtoSelector);
		}

		public static IEnumerable<ITaskModel> FilterForTasksCompleted(IEnumerable<ITaskModel> tasks)
		{
			todayDate = DateTime.Today;

			Func<ITaskModel, bool> tasksCompletedPredicate = task =>
			task.Status == TaskStatusHelper.TaskStatusType.Completed &&
			task.DueDate.CompareTo(todayDate) > DateCompareValueEarlier;

			return ExecuteFilter(tasks, tasksCompletedPredicate, createNewTaskModelDtoSelector);
		}

		public static IEnumerable<ITaskModel> FilterForTasksNotCompleted(IEnumerable<ITaskModel> tasks, DateTime? filterDueDate)
		{
			todayDate = DateTime.Today;

			Func<ITaskModel, bool> tasksNotCompletedPredicate = task =>
			{
				if (filterDueDate is null)
				{
					return task.Status != TaskStatusHelper.TaskStatusType.Completed && task.DueDate.CompareTo(todayDate) > DateCompareValueEarlier;
				}

				return (task.Status != TaskStatusHelper.TaskStatusType.Completed) && (task.DueDate.CompareTo(filterDueDate) < DateCompareValueEarlier && task.DueDate.CompareTo(todayDate) > DateCompareValueEarlier);
			};

			return ExecuteFilter(tasks, tasksNotCompletedPredicate, createNewTaskModelDtoSelector);
		}

		public static IEnumerable<ITaskModel> FilterForTasksExpired(IEnumerable<ITaskModel> tasks)
		{
			todayDate = DateTime.Today;

			Func<ITaskModel, bool> TasksExpiredPredicate = t =>
			t.DueDate.CompareTo(todayDate) < DateCompareValueEarlier;

			return ExecuteFilter(tasks, TasksExpiredPredicate, createNewTaskModelDtoSelector);
		}
	}
}
