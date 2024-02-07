using App.Features.Tags.Common.Models;
using App.Features.Tasks.Common.Models;
using App.Features.Tasks.Common.TaskTags.Common.Interfaces;

namespace App.Features.Tasks.Common.TaskTags.Common;

public class TaskTagDto : ITaskTagDto
{
	public Guid TaskId { get; set; } = Guid.NewGuid();
	public TaskDto Task { get; set; } = new TaskDto();

	public Guid TagId { get; set; } = Guid.NewGuid();
	public TagDto Tag { get; set; } = new TagDto();
}
