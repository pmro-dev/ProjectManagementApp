using Microsoft.AspNetCore.Mvc.Rendering;
using Project_DomainEntities.Helpers;

namespace Project_Main.Models.Inputs.ViewModels
{
    public interface ITaskEditInputVM
    {
        int Id { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        DateTime DueDate { get; set; }
        DateTime? ReminderDate { get; set; }
        TaskStatusHelper.TaskStatusType Status { get; set; }
        SelectList? StatusSelector { get; set; }
        int TodoListId { get; set; }
        SelectList? TodoListSelector { get; set; }
        string UserId { get; set; }
    }
}