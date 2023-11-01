namespace Project_Main.Infrastructure.DTOs
{
    public interface ITaskCreationInputDto
    {
        string Description { get; set; }
        DateTime DueDate { get; set; }
        DateTime? ReminderDate { get; set; }
        string Title { get; set; }
        int TodoListId { get; set; }
        string UserId { get; set; }
    }
}