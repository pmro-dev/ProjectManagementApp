namespace Project_Main.Infrastructure.DTOs.Inputs
{
    public interface ITaskCreateInputDto
    {
        string Description { get; set; }
        DateTime DueDate { get; set; }
        DateTime? ReminderDate { get; set; }
        string Title { get; set; }
        int TodoListId { get; set; }
        string UserId { get; set; }
    }
}