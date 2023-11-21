using App.Features.Boards.Briefly;
using App.Features.Tasks.Common.Helpers;
using App.Features.Tasks.Common.Models;
using App.Features.TodoLists.Common.Models;

namespace App.Features.Boards.ShowBriefly;

/// <summary>
/// View shows short version of ToDoLists informations.
/// </summary>
public class BoardBrieflyOutputVM : IBoardBrieflyOutputVM
{
	private readonly int ValueIndicatesEquality = 0;

	public ICollection<TodoListDto> TodoLists { get; set; } = new List<TodoListDto>();

	/// <summary>
	/// Get completed tasks count.
	/// </summary>
	/// <param name="todoList">Targeted ToDoList oobject.</param>
	/// <returns>Completed Tasks number.</returns>
	public int GetNumberOfCompletedTasks(TodoListDto todoList)
	{
		return todoList.Tasks.Count(t => t.Status == TaskStatusHelper.TaskStatusType.Completed);
	}

	/// <summary>
	/// Get number of all tasks in a specific ToDoList.
	/// </summary>
	/// <param name="todoList">Targeted ToDoList oobject.</param>
	/// <returns>All Tasks count in a specific ToDoList.</returns>
	public int GetNumberOfAllTasks(TodoListDto todoList)
	{
		return todoList.Tasks.Count;
	}

	/// <summary>
	/// Check that a specific ToDoList has any Task's reminder set for a present day.
	/// </summary>
	/// <param name="todoList">Targeted ToDoList object.</param>
	/// <returns>True when ToDoList has any Task's reminder, otherwise false.</returns>
	public bool IsReminderForToday(TodoListDto todoList)
	{
		return todoList.Tasks.Any(t =>
		{
			if (t.ReminderDate != null)
			{
				var reminderDate = t.ReminderDate.Value.ToShortDateString();
				var todayDate = DateTime.Today.ToShortDateString();

				return IsSameDate(reminderDate, todayDate) && IsTaskNotCompleted(t);
			}

			return false;
		});
	}

	private bool IsSameDate(string fistDate, string secondDate)
	{
		return fistDate.CompareTo(secondDate) == ValueIndicatesEquality;
	}

	private static bool IsTaskNotCompleted(TaskDto task)
	{
		return task.Status != TaskStatusHelper.TaskStatusType.Completed;
	}
}
