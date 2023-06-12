
using Project_DomainEntities;

namespace Project_Main.Models.ViewModels.TodoListViewModels
{
    /// <summary>
    /// Model for showing ToDoList.
    /// </summary>
    public class TodoListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public List<TaskModel> TasksForToday { get; set; } = new();
        public List<TaskModel> TasksCompleted { get; set; } = new();
        public List<TaskModel> TasksNotCompleted { get; set; } = new();
        public List<TaskModel> TasksExpired { get; set; } = new();
    }
}
