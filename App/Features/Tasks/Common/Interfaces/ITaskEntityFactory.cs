using App.Common.Interfaces;
using App.Features.Tags.Common.Interfaces;
using App.Features.Tasks.Common.Models;
using App.Features.Tasks.Common.TaskTags.Common.Interfaces;
using App.Features.Tasks.Delete.Models;
using App.Features.Tasks.Edit.Models;

namespace App.Features.Tasks.Common.Interfaces;

public interface ITaskEntityFactory : IBaseEntityFactory<TaskModel, TaskDto>, ITagFactory, ITaskTagFactory
{
	TaskDeleteInputDto CreateDeleteInputDto();
	TaskEditInputDto CreateEditInputDto();
	TaskModel CreateTaskModel(TaskModel originTask);
}
