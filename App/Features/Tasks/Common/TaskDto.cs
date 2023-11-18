using App.Common.Helpers;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.TaskTags.Common;
using App.Features.TodoLists.Common.Models;
using System.ComponentModel.DataAnnotations;
using static App.Features.Tasks.Common.TaskStatusHelper;

namespace App.Features.Tasks.Common;

public class TaskDto : ITaskDto
{
	private const int defaultId = 0;

	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	public DateTime DueDate { get; set; } = DateTime.Now;

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	public DateTime CreationDate { get; set; } = DateTime.Now;

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	public DateTime LastModificationDate { get; set; } = DateTime.Now;

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	public DateTime? ReminderDate { get; set; } = null;

	public TaskStatusType Status { get; set; } = TaskStatusType.NotStarted;

	public int TodoListId { get; set; } = defaultId;

	public TodoListDto? TodoList { get; set; }

	public string UserId { get; set; } = string.Empty;

	public ICollection<TaskTagDto> TaskTags { get; set; } = new List<TaskTagDto>();
}