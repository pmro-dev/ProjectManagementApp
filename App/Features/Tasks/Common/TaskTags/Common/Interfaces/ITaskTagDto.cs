using App.Features.Tags.Common.Interfaces;
using App.Features.Tasks.Common.Interfaces;

namespace App.Features.Tasks.Common.TaskTags.Common.Interfaces;

public interface ITaskTagDto
{
	ITagDto Tag { get; set; }

	int TagId { get; set; }

	ITaskDto Task { get; set; }

	int TaskId { get; set; }
}