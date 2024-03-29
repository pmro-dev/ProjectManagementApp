﻿using App.Common.Helpers;
using App.Features.Tasks.Common.Helpers;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Tasks.Common.Interfaces;

public interface ITaskCommonOutputVM
{
	int Id { get; set; }
	string Title { get; set; }
	string Description { get; set; }
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	DateTime CreationDate { get; set; }
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	DateTime DueDate { get; set; }
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	DateTime LastModificationDate { get; set; }
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	DateTime? ReminderDate { get; set; }
	TaskStatusHelper.TaskStatusType Status { get; set; }
	int TodoListId { get; set; }
	string UserId { get; set; }
}