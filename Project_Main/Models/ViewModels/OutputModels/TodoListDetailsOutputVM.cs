using Project_Main.Infrastructure.DTOs.Entities;

namespace Project_Main.Models.ViewModels.OutputModels
{
    /// <summary>
    /// Model for showing ToDoList.
    /// </summary>
    public class TodoListDetailsOutputVM : ITodoListDetailsOutputVM
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public IEnumerable<ITaskDto> TasksForToday { get; set; } = new List<ITaskDto>();
        public IEnumerable<ITaskDto> TasksCompleted { get; set; } = new List<ITaskDto>();
        public IEnumerable<ITaskDto> TasksNotCompleted { get; set; } = new List<ITaskDto>();
        public IEnumerable<ITaskDto> TasksExpired { get; set; } = new List<ITaskDto>();
    }
}
