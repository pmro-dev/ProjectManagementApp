using App.Features.Tags.Common.Models;
using App.Features.Tasks.Common.Models;
using App.Features.Tasks.Common.TaskTags.Common.Interfaces;

namespace App.Features.Tasks.Common.TaskTags.Common;

public class TaskTagDto : ITaskTagDto
{
	public int TaskId { get; set; }
	public TaskDto Task { get; set; } = new TaskDto();

	public int TagId { get; set; }
	public TagDto Tag { get; set; } = new TagDto();
}
