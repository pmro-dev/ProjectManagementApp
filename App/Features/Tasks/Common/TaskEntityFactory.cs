using App.Features.Tags.Common.Models;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.Models;
using App.Features.Tasks.Common.TaskTags.Common;
using App.Features.Tasks.Delete.Models;
using App.Features.Tasks.Edit.Models;

namespace App.Features.Tasks.Common;

public class TaskEntityFactory : ITaskEntityFactory
{
	public TaskDeleteInputDto CreateDeleteInputDto()
	{
		return new TaskDeleteInputDto();
	}

	public TaskModel CreateModel()
	{
		return new TaskModel();
	}

	public TaskDto CreateDto()
	{
		return new TaskDto();
	}

	public TaskEditInputDto CreateEditInputDto()
	{
		return new TaskEditInputDto();
	}

	public TagDto CreateTagDto()
	{
		return new TagDto();
	}

	public TagModel CreateTagModel()
	{
		return new TagModel();
	}

	public TaskTagDto CreateTaskTagDto()
	{
		return new TaskTagDto();
	}

	public TaskTagModel CreateTaskTagModel()
	{
		return new TaskTagModel();
	}
}
