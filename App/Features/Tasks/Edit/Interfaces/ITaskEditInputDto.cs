using App.Common.Helpers;
using System.ComponentModel.DataAnnotations;
using static App.Features.Tasks.Common.Helpers.TaskStatusHelper;

namespace App.Features.Tasks.Edit.Interfaces;

public interface ITaskEditInputDto
{
	Guid Id { get; set; }

	string Title { get; set; }

	string Description { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	DateTime Deadline { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	DateTime? ReminderDate { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	DateTime? LastModificationDate { get; set; }

	TaskStatusType Status { get; set; }
	Guid TodoListId { get; set; }
	string OwnerId { get; set; }
}