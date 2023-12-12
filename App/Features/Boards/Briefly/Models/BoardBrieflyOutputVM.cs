using App.Features.Boards.Briefly.Interfaces;
using App.Features.Pagination;
using App.Features.Tasks.Common.Helpers;
using App.Features.Tasks.Common.Models;
using App.Features.TodoLists.Common.Models;

namespace App.Features.Boards.Briefly.Models;

/// <summary>
/// View shows short version of ToDoLists informations.
/// </summary>
public class BoardBrieflyOutputVM : IBoardBrieflyOutputVM
{
    private readonly int ValueIndicatedEquality = 0;

    public List<Tuple<TodoListDto, int, int>> TupleDtos { get; }

    public PaginationData PaginData { get; }

	public BoardBrieflyOutputVM(List<Tuple<TodoListDto, int, int>> tupleDtos, PaginationData paginData)
	{
		TupleDtos = tupleDtos;
		PaginData = paginData;
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
        return fistDate.CompareTo(secondDate) == ValueIndicatedEquality;
    }

    private static bool IsTaskNotCompleted(TaskDto task)
    {
        return task.Status != TaskStatusHelper.TaskStatusType.Completed;
    }
}
