
using Project_DomainEntities;
using Project_DTO;

namespace Project_Main.Models.ViewModels.OutputModels
{
    /// <summary>
    /// Model for showing ToDoList.
    /// </summary>
    public class BoardsSingleDetailsOutputVM
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public List<ITaskDto> TasksForToday { get; set; } = new();
        public List<ITaskDto> TasksCompleted { get; set; } = new();
        public List<ITaskDto> TasksNotCompleted { get; set; } = new();
        public List<ITaskDto> TasksExpired { get; set; } = new();
    }
}
