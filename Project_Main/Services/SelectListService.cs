using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Main.Models.DTOs;
using static Project_DomainEntities.Helpers.TaskStatusHelper;

namespace Project_Main.Services
{
	public class SelectListService : ISelectListService
	{
		private const string _dataValueField = "Value";
		private const string _dataTextField = "Text";

		public SelectList CreateTaskStatusSelector(ITaskDto taskDto)
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

		public SelectList CreateTodoListSelector(ICollection<ITodoListDto> userTodoListDtos, int defaultSelectedTodoListId)
		{
			SelectList todoListSelectorDto = new(userTodoListDtos, nameof(ITodoListDto.Id), nameof(ITodoListDto.Title), defaultSelectedTodoListId);

			return todoListSelectorDto;
		}
	}
}
