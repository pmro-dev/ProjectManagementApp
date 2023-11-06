using Project_DomainEntities;
using Project_Main.Models.DTOs;
using Project_Main.Models.Inputs.DTOs;
using Project_Main.Models.Inputs.ViewModels;

namespace Project_Main.Services.DTO
{
    public interface ITaskEntityMapper
    {
        ITaskDto TransferToDto(ITaskModel taskModel, IDictionary<object, object>? mappedObjects = null);
        //ITaskEditOutputDto TransferToDto(ITaskModel taskModel, SelectList todoListSelector, SelectList taskStatusSelector);
        ITaskEditInputDto TransferToDto(ITaskEditInputVM taskEditInputVM);
        ITaskDeleteInputDto TransferToDto(ITaskDeleteInputVM deleteInputVM);
        ITaskDto TransferToDto(ITaskCreateInputVM taskInputVM);
        void UpdateModel(ITaskModel taskDbModel, ITaskEditInputDto taskEditInputDto);
        ITaskModel TransferToModel(ITaskDto taskDto, IDictionary<object, object>? mappedObjects = null);
    }
}