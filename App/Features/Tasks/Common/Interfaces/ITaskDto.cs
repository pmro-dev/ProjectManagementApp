using App.Features.Tasks.Common.TaskTags.Common;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static App.Features.Tasks.Common.Helpers.TaskStatusHelper;

namespace App.Features.Tasks.Common.Interfaces;

public interface ITaskDto
{
	[Required]
	[Key]
	public Guid Id { get; set; }

	[Required]
	public string Title { get; set; }

	[Required]
	public string Description { get; set; }

	[Required]
	public DateTime Deadline { get; set; }

	public DateTime Created { get; set; }

	public DateTime LastModified { get; set; }

	public DateTime? ReminderDate { get; set; }

	public TaskStatusType Status { get; set; }

	[Required]
	public Guid TodoListId { get; set; }

	[ForeignKey(nameof(TodoListId))]
	public TodoListDto? TodoList { get; set; }

	[Required]
	public string OwnerId { get; set; }

	[ForeignKey(nameof(OwnerId))]
	public UserModel? Owner { get; set; }

	public ICollection<TaskTagDto> TaskTags { get; set; }
}