using Project_Main.Infrastructure.DTOs.Entities;

namespace Project_Main.Models.ViewModels.OutputModels
{
    public interface ITodoListDetailsOutputVM
    {
        int Id { get; set; }
        string Name { get; set; }
		ICollection<ITaskDto> TasksCompleted { get; set; }
		ICollection<ITaskDto> TasksExpired { get; set; }
		ICollection<ITaskDto> TasksForToday { get; set; }
		ICollection<ITaskDto> TasksNotCompleted { get; set; }
        string UserId { get; set; }
    }
}