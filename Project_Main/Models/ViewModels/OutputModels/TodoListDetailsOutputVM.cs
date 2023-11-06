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

        public ICollection<ITaskDto> TasksForToday { get; set; } = new List<ITaskDto>();
        public ICollection<ITaskDto> TasksCompleted { get; set; } = new List<ITaskDto>();
        public ICollection<ITaskDto> TasksNotCompleted { get; set; } = new List<ITaskDto>();
        public ICollection<ITaskDto> TasksExpired { get; set; } = new List<ITaskDto>();
    }
}
