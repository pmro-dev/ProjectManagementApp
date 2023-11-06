using Project_DomainEntities.Helpers;

namespace Project_Main.Models.Inputs.DTOs
{
    public class TaskEditInputDto : ITaskEditInputDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public DateTime? ReminderDate { get; set; }
        public TaskStatusHelper.TaskStatusType Status { get; set; }
        public int TodoListId { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
