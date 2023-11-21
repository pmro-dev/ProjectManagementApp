using App.Features.Tasks.Common.TaskTags.Common;
using App.Features.TodoLists.Common.Models;
using static App.Features.Tasks.Common.Helpers.TaskStatusHelper;

namespace App.Features.Tasks.Common.Interfaces;

public interface ITaskDto
{
	public int Id { get; set; }

	public string Title { get; set; }

	public string Description { get; set; }

	public DateTime DueDate { get; set; }

	public DateTime CreationDate { get; set; }

	public DateTime LastModificationDate { get; set; }

	public DateTime? ReminderDate { get; set; }

	public TaskStatusType Status { get; set; }

	public int TodoListId { get; set; }

	public TodoListDto? TodoList { get; set; }

	public string UserId { get; set; }

	public ICollection<TaskTagDto> TaskTags { get; set; }
}