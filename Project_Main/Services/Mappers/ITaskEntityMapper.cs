using Microsoft.AspNetCore.Mvc.Rendering;
using Project_DomainEntities;
using Project_Main.Infrastructure.DTOs.Entities;
using Project_Main.Infrastructure.DTOs.Inputs;
using Project_Main.Infrastructure.DTOs.Outputs;
using Project_Main.Models.ViewModels.InputModels;

namespace Project_Main.Services.DTO
{
    public interface ITaskEntityMapper
    {
        ITaskDto TransferToDto(ITaskModel taskModel, IDictionary<object, object>? mappedObjects = null);
        ITaskEditOutputDto TransferToDto(ITaskModel taskModel, SelectList todoListSelector, SelectList taskStatusSelector);
        ITaskEditInputDto TransferToDto(ITaskEditInputVM taskEditInputVM);
        ITaskDeleteInputDto TransferToDto(ITaskDeleteInputVM deleteInputVM);
        ITaskDto TransferToDto(ITaskCreateInputVM taskInputVM);
        void UpdateModel(ITaskModel taskDbModel, ITaskEditInputDto taskEditInputDto);
        ITaskModel TransferToModel(ITaskDto taskDto, IDictionary<object, object>? mappedObjects = null);
    }
}