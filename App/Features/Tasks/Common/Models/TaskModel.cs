using App.Common.Helpers;
using App.Features.Tasks.Common.Helpers;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.TaskTags.Common;
using App.Features.TodoLists.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static App.Features.Tasks.Common.Helpers.TaskStatusHelper;

namespace App.Features.Tasks.Common.Models;

/// <summary>
/// Model for Task.
/// </summary>
public class TaskModel : ITaskModel
{
    private const int defaultId = 0;

    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(TaskAttributesHelper.TitleMaxLength)]
    [MinLength(TaskAttributesHelper.TitleMinLength)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.MultilineText)]
    [MaxLength(TaskAttributesHelper.DescriptionMaxLength)]
    [MinLength(TaskAttributesHelper.DescriptionMinLength)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
    public DateTime DueDate { get; set; } = DateTime.Now;

    [Required]
    [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
    public DateTime CreationDate { get; set; } = DateTime.Now;

    [Required]
    [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
    public DateTime LastModificationDate { get; set; } = DateTime.Now;

    [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
    public DateTime? ReminderDate { get; set; } = null;

    [Required]
    public TaskStatusType Status { get; set; } = TaskStatusType.NotStarted;

    public ICollection<TaskTagModel> TaskTags { get; set; } = new List<TaskTagModel>();

    [Required]
    public int TodoListId { get; set; } = defaultId;

    [ForeignKey(nameof(TodoListId))]
    public virtual TodoListModel? TodoList { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;
}
