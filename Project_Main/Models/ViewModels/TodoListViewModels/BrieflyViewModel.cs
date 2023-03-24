using Project_DomainEntities;
using Project_DomainEntities.Helpers;

namespace Project_Main.Models.ViewModels.TodoListViewModels
{
    public class BrieflyViewModel
    {
        private readonly int ValueIndicatesEquality = 0;

        public List<TodoListModel> TodoLists { get; set; } = new List<TodoListModel>();

        public int GetNumberOfCompletedTasks(TodoListModel todoList)
        {
            return todoList.Tasks.Count(t => t.Status == TaskStatusHelper.TaskStatusType.Completed);
        }

        public int GetNumberOfAllTasks(TodoListModel todoList)
        {
            return todoList.Tasks.Count;
        }

        public bool IsReminderForToday(TodoListModel todoList)
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

        private static bool IsTaskNotCompleted(TaskModel task)
        {
            return task.Status != TaskStatusHelper.TaskStatusType.Completed;
        }
    }
}
