using App.Features.Tags.Common.Interfaces;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.TaskTags.Common.Interfaces;
using App.Features.Tags.Common;

namespace App.Features.Tasks.Common.TaskTags.Common;

public class TaskTagModel : ITaskTagModel
{
	public int TaskId { get; set; }
	public ITaskModel Task { get; set; } = new TaskModel();

	public int TagId { get; set; }
	public ITagModel Tag { get; set; } = new TagModel();
}
