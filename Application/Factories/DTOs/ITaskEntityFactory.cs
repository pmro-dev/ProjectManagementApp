using Application.DTOs.Entities;
using Application.DTOs.ForViewModels.Inputs;
using Domain.Entities;

namespace Application.Factories.DTOs;

public interface ITaskEntityFactory : IBaseEntityFactory<TaskModel, TaskDto>, ITagFactory, ITaskTagFactory
{
	TaskDeleteInputDto CreateDeleteInputDto();
	TaskEditInputDto CreateEditInputDto();
}
