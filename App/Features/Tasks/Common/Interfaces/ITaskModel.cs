using App.Common.Helpers;
using App.Common.Interfaces;
using App.Features.Tasks.Common.TaskTags.Common;
using App.Features.TodoLists.Common.Models;
using System.ComponentModel.DataAnnotations;
using static App.Features.Tasks.Common.Helpers.TaskStatusHelper;

namespace App.Features.Tasks.Common.Interfaces;

public interface ITaskModel : IBasicModelAbstract<int>
{
	private const string DataFormat = AttributesHelper.DataFormat;

	public string Description { get; set; }

	[DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
	public DateTime DueDate { get; set; }

	[DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
	public DateTime CreationDate { get; set; }

	[DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
	public DateTime LastModificationDate { get; set; }

	[DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
	public DateTime? ReminderDate { get; set; }

	public TaskStatusType Status { get; set; }

	public int TodoListId { get; set; }

	public TodoListModel? TodoList { get; set; }

	public string UserId { get; set; }

	public ICollection<TaskTagModel> TaskTags { get; set; }
}
