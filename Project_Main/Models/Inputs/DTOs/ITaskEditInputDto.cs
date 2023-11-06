using Project_DomainEntities.Helpers;

namespace Project_Main.Models.Inputs.DTOs
{
    public interface ITaskEditInputDto
    {
        int Id { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        DateTime DueDate { get; set; }
        DateTime? ReminderDate { get; set; }
        TaskStatusHelper.TaskStatusType Status { get; set; }
        int TodoListId { get; set; }
        string UserId { get; set; }
    }
}