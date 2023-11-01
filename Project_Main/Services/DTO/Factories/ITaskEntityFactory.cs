using Project_DomainEntities;
using Project_DTO;
using Project_Main.Infrastructure.DTOs;
using Project_Main.Models.ViewModels.InputModels;

namespace Project_Main.Services.DTO.Builders
{
    public interface ITaskEntityFactory : IEntityFactory<ITaskModel, ITaskDto>
    {
        public ITaskModel CreateModel(ITaskCreationInputDto taskCreateInputDto);
        public ITaskCreationInputDto CreateDto(ITaskCreationInputVM taskCreateInputVM);
    }
}