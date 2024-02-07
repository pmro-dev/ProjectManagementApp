using App.Common.Helpers;
using App.Features.Tasks.Common.Helpers;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Tasks.Common.Interfaces;

public interface ITaskCommonOutputVM
{
	Guid Id { get; set; }
	string Title { get; set; }
	string Description { get; set; }
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	DateTime Created { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	DateTime Deadline { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	DateTime LastModified { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	DateTime? ReminderDate { get; set; }

	TaskStatusHelper.TaskStatusType Status { get; set; }

	Guid TodoListId { get; set; }

	string UserId { get; set; }
}