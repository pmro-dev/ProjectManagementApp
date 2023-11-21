using App.Common.Helpers;
using App.Features.Tasks.Show.Interfaces;
using System.ComponentModel.DataAnnotations;
using static App.Features.Tasks.Common.Helpers.TaskStatusHelper;

namespace App.Features.Tasks.Show;

public class ShowTaskOutputVM : IShowTaskOutputVM
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;

	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	public DateTime DueDate { get; set; } = DateTime.Now;

	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	public DateTime CreationDate { get; set; } = DateTime.Now;

	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	public DateTime LastModificationDate { get; set; } = DateTime.Now;

	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	public DateTime? ReminderDate { get; set; } = null;

	public TaskStatusType Status { get; set; } = TaskStatusType.NotStarted;

	public int TodoListId { get; set; }

	public string UserId { get; set; } = string.Empty;
}
