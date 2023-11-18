using App.Features.Tasks.Common.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using static App.Features.Tasks.Common.TaskStatusHelper;

namespace App.Features.Tasks.Common;

public class TaskSelector : ITaskSelector
{
	private const string _dataValueField = "Value";
	private const string _dataTextField = "Text";

	public SelectList Create(TaskDto taskDto)
	{
		var statusTypesForSelector = Enum.GetValues(typeof(TaskStatusType))
				.Cast<TaskStatusType>()
				.Select(taskStatusType => new SelectListItem
				{
					Text = taskStatusType.ToString(),
					Value = ((int)taskStatusType).ToString()
				})
				.ToList();

		SelectList taskStatusSelectorDto = new(statusTypesForSelector, _dataValueField, _dataTextField, taskDto.Status);

		return taskStatusSelectorDto;
	}
}
