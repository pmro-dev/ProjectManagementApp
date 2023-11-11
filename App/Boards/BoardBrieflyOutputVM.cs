using Web.Boards.Interfaces;
using Web.Tasks.Common;
using Web.Tasks.Common.Interfaces;
using Web.TodoLists.Common.Interfaces;

namespace Web.Boards
{
	/// <summary>
	/// View shows short version of ToDoLists informations.
	/// </summary>
	public class BoardBrieflyOutputVM : IBoardBrieflyOutputVM
    {
        private readonly int ValueIndicatesEquality = 0;

        public ICollection<ITodoListDto> TodoLists { get; set; } = new List<ITodoListDto>();

        /// <summary>
        /// Get completed tasks count.
        /// </summary>
        /// <param name="todoList">Targeted ToDoList oobject.</param>
        /// <returns>Completed Tasks number.</returns>
        public int GetNumberOfCompletedTasks(ITodoListDto todoList)
        {
            return todoList.Tasks.Count(t => t.Status == TaskStatusHelper.TaskStatusType.Completed);
        }

        /// <summary>
        /// Get number of all tasks in a specific ToDoList.
        /// </summary>
        /// <param name="todoList">Targeted ToDoList oobject.</param>
        /// <returns>All Tasks count in a specific ToDoList.</returns>
        public int GetNumberOfAllTasks(ITodoListDto todoList)
        {
            return todoList.Tasks.Count;
        }

        /// <summary>
        /// Check that a specific ToDoList has any Task's reminder set for a present day.
        /// </summary>
        /// <param name="todoList">Targeted ToDoList object.</param>
        /// <returns>True when ToDoList has any Task's reminder, otherwise false.</returns>
        public bool IsReminderForToday(ITodoListDto todoList)
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

        private static bool IsTaskNotCompleted(ITaskDto task)
        {
            return task.Status != TaskStatusHelper.TaskStatusType.Completed;
        }
    }
}
