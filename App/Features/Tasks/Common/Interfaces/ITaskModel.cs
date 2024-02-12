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

interface ITaskModel : IBasicModelWithTitle
{
	private const string DataFormat = AttributesHelper.DataFormat;

	[Timestamp]
	byte[] RowVersion { get; set; }

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

	[Required]
	string OwnerId { get; set; }

	[ForeignKey(nameof(OwnerId))]
	UserModel? Owner { get; set; }

	ICollection<TaskTagModel> TaskTags { get; set; }
}
