using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Tasks.Common.Interfaces;
using static Web.Tasks.Common.TaskStatusHelper;

namespace Web.Tasks.Common
{
	public class TaskSelector : ITaskSelector
	{
		private const string _dataValueField = "Value";
		private const string _dataTextField = "Text";

		public SelectList Create(ITaskDto taskDto)
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
}
