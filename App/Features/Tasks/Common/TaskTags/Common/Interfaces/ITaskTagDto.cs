using App.Features.Tags.Common.Models;
using App.Features.Tasks.Common.Models;

namespace App.Features.Tasks.Common.TaskTags.Common.Interfaces;

public interface ITaskTagDto
{
	TagDto Tag { get; set; }

	int TagId { get; set; }

	TaskDto Task { get; set; }

	int TaskId { get; set; }
}