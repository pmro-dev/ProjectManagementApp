#region USINGS
using App.Common.Helpers;
using App.Common.Interfaces;
using App.Features.Tags.Common.Models;
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
	string Description { get; set; }

	[Required]
	[DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
	DateTime Deadline { get; set; }

	[Required]
	[DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
	DateTime Created { get; set; }

	[Required]
	[DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
	DateTime LastModified { get; set; }

	[DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
	DateTime? ReminderDate { get; set; }

	[Required]
	TaskStatusType Status { get; set; }

	[Required]
	Guid TodoListId { get; set; }

	[ForeignKey(nameof(TodoListId))]
	TodoListModel? TodoList { get; set; }

	string? OwnerId { get; set; }
	//UserModel? Owner { get; set; }

	ICollection<TagModel> Tags { get; set; }
	ICollection<TaskTagModel> TaskTags { get; set; }
}
