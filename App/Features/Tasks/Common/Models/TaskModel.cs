#region USINGS
using App.Common.Helpers;
using App.Features.Tags.Common.Models;
using App.Features.Tasks.Common.Helpers;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.TaskTags.Common;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static App.Features.Tasks.Common.Helpers.TaskStatusHelper;
#endregion

namespace App.Features.Tasks.Common.Models;

/// <summary>
/// Model for Task.
/// </summary>
public class TaskModel : ITaskModel
{
	[Key]
	[Required]
	public Guid Id { get; set; } = Guid.NewGuid();

	[Required]
	[MaxLength(TaskAttributesHelper.TitleMaxLength)]
	[MinLength(TaskAttributesHelper.TitleMinLength)]
	public string Title { get; set; } = string.Empty;

	[Timestamp]
	public byte[] RowVersion { get; set; } = { 1, 1, 1 };

	[Required]
	[DataType(DataType.MultilineText)]
	[MaxLength(TaskAttributesHelper.DescriptionMaxLength)]
	[MinLength(TaskAttributesHelper.DescriptionMinLength)]
	public string Description { get; set; } = string.Empty;

	[Required]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	public DateTime Deadline { get; set; } = DateTime.Now;

	[Required]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	public DateTime Created { get; set; } = DateTime.Now;

	[Required]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	public DateTime LastModified { get; set; } = DateTime.Now;

	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	public DateTime? ReminderDate { get; set; } = null;

	[Required]
	public TaskStatusType Status { get; set; } = TaskStatusType.NotStarted;

	public ICollection<TagModel> Tags { get; set; } = new List<TagModel>();
	public ICollection<TaskTagModel> TaskTags { get; set; } = new List<TaskTagModel>();

	[Required]
	public Guid TodoListId { get; set; } = Guid.Empty;

	[ForeignKey(nameof(TodoListId))]
	public virtual TodoListModel? TodoList { get; set; }

	public string? OwnerId { get; set; } = string.Empty;

	[ForeignKey(nameof(OwnerId))]
	public virtual UserModel? Owner { get; set; }
}
