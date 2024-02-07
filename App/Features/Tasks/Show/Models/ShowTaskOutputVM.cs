using App.Common.Helpers;
using App.Features.Tasks.Show.Interfaces;
using System.ComponentModel.DataAnnotations;
using static App.Features.Tasks.Common.Helpers.TaskStatusHelper;

namespace App.Features.Tasks.Show.Models;

public class ShowTaskOutputVM : IShowTaskOutputVM
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
    public DateTime Deadline { get; set; } = DateTime.Now;

    [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
    public DateTime Created { get; set; } = DateTime.Now;

    [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
    public DateTime LastModified { get; set; } = DateTime.Now;

    [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
    public DateTime? ReminderDate { get; set; } = null;

    public TaskStatusType Status { get; set; } = TaskStatusType.NotStarted;

    public Guid TodoListId { get; set; } = Guid.Empty;

    public string UserId { get; set; } = string.Empty;
}
