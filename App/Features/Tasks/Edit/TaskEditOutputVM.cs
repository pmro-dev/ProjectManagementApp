using App.Common.Helpers;
using App.Features.Tasks.Edit.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static App.Features.Tasks.Common.TaskStatusHelper;

namespace App.Features.Tasks.Edit;

public class TaskEditOutputVM : ITaskEditOutputVM
{
	private const int defaultId = 0;

	public int Id { get; set; }

	public string UserId { get; set; } = string.Empty;

	public string Title { get; set; } = string.Empty;

	[DataType(DataType.MultilineText)]
	public string Description { get; set; } = string.Empty;

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	public DateTime DueDate { get; set; } = DateTime.Now;

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	public DateTime? ReminderDate { get; set; } = null;

	public TaskStatusType Status { get; set; } = TaskStatusType.NotStarted;

	public int TodoListId { get; set; } = defaultId;

	public SelectList? StatusSelector { get; set; }

	public SelectList? TodoListSelector { get; set; }
}
