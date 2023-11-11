using Web.Common.Interfaces;
using Web.Tags.Common.Interfaces;
using Web.Tasks.Delete;
using Web.Tasks.Edit;
using Web.TaskTags.Common.Interfaces;

namespace Web.Tasks.Common.Interfaces;

public interface ITaskEntityFactory : IBaseEntityFactory<TaskModel, TaskDto>, ITagFactory, ITaskTagFactory
{
	TaskDeleteInputDto CreateDeleteInputDto();
	TaskEditInputDto CreateEditInputDto();
}
