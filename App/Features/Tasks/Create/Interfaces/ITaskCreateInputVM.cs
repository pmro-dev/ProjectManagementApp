using App.Common.Helpers;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Tasks.Create.Interfaces;

public interface ITaskCreateInputVM
{
	string Description { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	DateTime Deadline { get; set; }
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]

	DateTime? ReminderDate { get; set; }

	string Title { get; set; }

	Guid TodoListId { get; set; }

	string UserId { get; set; }
}