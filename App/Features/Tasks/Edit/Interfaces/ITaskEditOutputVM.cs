using App.Common.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static App.Features.Tasks.Common.Helpers.TaskStatusHelper;

namespace App.Features.Tasks.Edit.Interfaces;

public interface ITaskEditOutputVM
{
	Guid Id { get; set; }

	string Description { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	DateTime Deadline { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	DateTime? ReminderDate { get; set; }

	TaskStatusType Status { get; set; }

	SelectList? StatusSelector { get; set; }

	string Title { get; set; }

	Guid TodoListId { get; set; }

	SelectList? TodoListSelector { get; set; }

	string OwnerId { get; set; }
}