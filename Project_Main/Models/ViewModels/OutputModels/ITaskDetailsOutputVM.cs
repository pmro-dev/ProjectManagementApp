using Project_DomainEntities.Helpers;

namespace Project_Main.Models.ViewModels.OutputModels
{
    public interface ITaskDetailsOutputVM : ITaskViewModel
    {
        DateTime CreationDate { get; set; }
        string Description { get; set; }
        DateTime DueDate { get; set; }
        int Id { get; set; }
        DateTime LastModificationDate { get; set; }
        DateTime? ReminderDate { get; set; }
        TaskStatusHelper.TaskStatusType Status { get; set; }
        string Title { get; set; }
        int TodoListId { get; set; }
        string UserId { get; set; }
    }
}