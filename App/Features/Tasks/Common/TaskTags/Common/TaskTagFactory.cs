using App.Features.Tasks.Common.TaskTags.Common.Interfaces;

namespace App.Features.Tasks.Common.TaskTags.Common;

public class TaskTagFactory : ITaskTagFactory
{
	public TaskTagDto CreateDto()
	{
		return new TaskTagDto();
	}

	public TaskTagModel CreateModel()
	{
		return new TaskTagModel();
	}
}
