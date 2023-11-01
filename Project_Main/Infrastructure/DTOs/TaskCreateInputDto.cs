namespace Project_Main.Infrastructure.DTOs
{
    public class TaskCreateInputDto : ITaskCreationInputDto
    {
        public int TodoListId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime DueDate { get; set; } = DateTime.Now;

        public DateTime? ReminderDate { get; set; } = null;
    }
}
