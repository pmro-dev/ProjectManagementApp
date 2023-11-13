using App.Common.Helpers;
using App.Features.Tasks.Common;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Tasks.Edit.Interfaces;

public interface ITaskEditInputDto
{
	int Id { get; set; }
	string Title { get; set; }
	string Description { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	DateTime DueDate { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	DateTime? ReminderDate { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	DateTime? LastModificationDate { get; set; }

	TaskStatusHelper.TaskStatusType Status { get; set; }
	int TodoListId { get; set; }
	string UserId { get; set; }
}