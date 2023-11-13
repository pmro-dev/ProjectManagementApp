using App.Features.Tasks.Common.TaskTags.Common.Interfaces;
using App.Features.TodoLists.Common.Interfaces;
using static App.Features.Tasks.Common.TaskStatusHelper;

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

	public ITodoListDto? TodoList { get; set; }

	public string UserId { get; set; }

	public ICollection<ITaskTagDto> TaskTags { get; set; }
}