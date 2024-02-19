using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.Models;
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
}
