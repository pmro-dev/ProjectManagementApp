namespace Project_Main.Models.ViewModels.InputModels
{
    public interface ITaskCreateInputVM
    {
        string Description { get; set; }
        DateTime DueDate { get; set; }
        DateTime? ReminderDate { get; set; }
        string Title { get; set; }
        int TodoListId { get; set; }
        string UserId { get; set; }
    }
}