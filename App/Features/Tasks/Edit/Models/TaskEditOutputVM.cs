using App.Common.Helpers;
using App.Features.Tasks.Edit.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static App.Features.Tasks.Common.Helpers.TaskStatusHelper;

namespace App.Features.Tasks.Edit.Models;

public class TaskEditOutputVM : ITaskEditOutputVM
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string OwnerId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    [DataType(DataType.MultilineText)]
    public string Description { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
    public DateTime Deadline { get; set; } = DateTime.Now;

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
    public DateTime? ReminderDate { get; set; } = null;

    public TaskStatusType Status { get; set; } = TaskStatusType.NotStarted;

    public Guid TodoListId { get; set; } = Guid.NewGuid();

    public SelectList? StatusSelector { get; set; }

    public SelectList? TodoListSelector { get; set; }
}
