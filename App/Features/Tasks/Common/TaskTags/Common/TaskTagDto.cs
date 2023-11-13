using App.Features.Tags.Common;
using App.Features.Tags.Common.Interfaces;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.TaskTags.Common.Interfaces;

namespace App.Features.Tasks.Common.TaskTags.Common;

public class TaskTagDto : ITaskTagDto
{
	public int TaskId { get; set; }
	public ITaskDto Task { get; set; } = new TaskDto();

	public int TagId { get; set; }
	public ITagDto Tag { get; set; } = new TagDto();
}
