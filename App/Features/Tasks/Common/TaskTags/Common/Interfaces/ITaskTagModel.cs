using App.Features.Tags.Common.Interfaces;
using App.Features.Tasks.Common.Interfaces;

namespace App.Features.Tasks.Common.TaskTags.Common.Interfaces;

public interface ITaskTagModel
{
	ITagModel Tag { get; set; }
	int TagId { get; set; }
	ITaskModel Task { get; set; }
	int TaskId { get; set; }
}