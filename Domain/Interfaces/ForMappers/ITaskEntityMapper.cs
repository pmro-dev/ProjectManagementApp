using Project_DomainEntities;
using Project_Main.Models.DTOs;
using Project_Main.Models.Inputs.DTOs;
using Project_Main.ViewModels.Inputs.Abstract;

namespace Project_Main.Services.DTO
{
    public interface ITaskEntityMapper
    {
        ITaskDto TransferToDto(TaskModel taskModel, IDictionary<object, object>? mappedObjects = null);
        ITaskEditInputDto TransferToDto(ITaskEditInputVM taskEditInputVM);
        ITaskDeleteInputDto TransferToDto(ITaskDeleteInputVM deleteInputVM);
        ITaskDto TransferToDto(ITaskCreateInputVM taskInputVM);
        void UpdateModel(TaskModel taskDbModel, ITaskEditInputDto taskEditInputDto);
        TaskModel TransferToModel(ITaskDto taskDto, IDictionary<object, object>? mappedObjects = null);
    }
}