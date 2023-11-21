using App.Features.Tasks.Common.TaskTags.Common.Interfaces;
using App.Features.Tasks.Common.Models;
using App.Features.Tags.Common.Models;

namespace App.Features.Tasks.Common.TaskTags.Common;

public class TaskTagModel : ITaskTagModel
{
	public int TaskId { get; set; }
	public TaskModel Task { get; set; } = new TaskModel();

	public int TagId { get; set; }
	public TagModel Tag { get; set; } = new TagModel();
}
