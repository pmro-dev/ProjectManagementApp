using Project_DomainEntities.Helpers;

namespace Project_Main.Infrastructure.DTOs.Inputs
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