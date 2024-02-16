using App.Features.Tags.Common.Models;
using App.Features.Tasks.Common.Models;

namespace App.Features.Tasks.Common.TaskTags.Common.Interfaces;

public interface ITaskTagDto
{
	byte[] RowVersion { get; set; }

	TagDto Tag { get; set; }

	Guid TagId { get; set; }

	TaskDto Task { get; set; }

	Guid TaskId { get; set; }
}