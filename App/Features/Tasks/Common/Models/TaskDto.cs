using App.Common.Helpers;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.TaskTags.Common;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static App.Features.Tasks.Common.Helpers.TaskStatusHelper;

namespace App.Features.Tasks.Common.Models;

public class TaskDto : ITaskDto
{
    [Required]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
	public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
    public DateTime Deadline { get; set; } = DateTime.Now;

    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
    public DateTime Created { get; set; } = DateTime.Now;

    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
    public DateTime LastModified { get; set; } = DateTime.Now;

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
    public DateTime? ReminderDate { get; set; } = null;

    [Required]
    public TaskStatusType Status { get; set; } = TaskStatusType.NotStarted;

    [Required]
    public Guid TodoListId { get; set; } = Guid.NewGuid();

    [ForeignKey(nameof(TodoListId))]
    public TodoListDto? TodoList { get; set; }

    [Required]
    public string OwnerId { get; set; } = string.Empty;

    [ForeignKey(nameof(OwnerId))]
    public virtual UserModel? Owner { get; set; }

    public ICollection<TaskTagDto> TaskTags { get; set; } = new List<TaskTagDto>();
}