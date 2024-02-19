using App.Common.Interfaces;
using App.Features.Tasks.Common.Models;
using App.Features.Tasks.Delete.Models;
using App.Features.Tasks.Edit.Models;

namespace App.Features.Tasks.Common.Interfaces;

public interface ITaskEntityFactory : IBaseEntityFactory<TaskModel, TaskDto>
{
	TaskDeleteInputDto CreateDeleteInputDto();
	TaskEditInputDto CreateEditInputDto();
}
