using Project_DomainEntities.Helpers;

namespace Project_Main.Models.Outputs.ViewModels
{
    public interface ITaskDetailsOutputVM
    {
        int Id { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        DateTime CreationDate { get; set; }
        DateTime DueDate { get; set; }
        DateTime LastModificationDate { get; set; }
        DateTime? ReminderDate { get; set; }
        TaskStatusHelper.TaskStatusType Status { get; set; }
        int TodoListId { get; set; }
        string UserId { get; set; }
    }
}