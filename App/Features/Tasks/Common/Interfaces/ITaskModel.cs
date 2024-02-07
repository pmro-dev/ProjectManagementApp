#region USINGS
using App.Common.Helpers;
using App.Common.Interfaces;
using App.Features.Tasks.Common.TaskTags.Common;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static App.Features.Tasks.Common.Helpers.TaskStatusHelper;
#endregion

namespace App.Features.Tasks.Common.Interfaces;

public interface ITaskModel : IBasicModelWithTitle
{
	private const string DataFormat = AttributesHelper.DataFormat;

	[Required]
	public string DataVersion { get; set; }

	[Required]
	public string Description { get; set; }

	[Required]
	[DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
	public DateTime Deadline { get; set; }

	[Required]
	[DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
	public DateTime Created { get; set; }

	[Required]
	[DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
	public DateTime LastModified { get; set; }

	[DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
	public DateTime? ReminderDate { get; set; }

	[Required]
	public TaskStatusType Status { get; set; }

	[Required]
	public Guid TodoListId { get; set; }

	[ForeignKey(nameof(TodoListId))]
	public TodoListModel? TodoList { get; set; }

	[Required]
	public string UserId { get; set; }

	[ForeignKey(nameof(UserId))]
	public UserModel? Owner { get; set; }

	public ICollection<TaskTagModel> TaskTags { get; set; }
}
