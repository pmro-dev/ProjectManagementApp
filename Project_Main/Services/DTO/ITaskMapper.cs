using Project_DomainEntities;
using Project_DTO;
using Project_Main.Infrastructure.DTOs;
using Project_Main.Models.ViewModels.InputModels;
using Project_Main.Models.ViewModels.OutputModels;

namespace Project_Main.Services.DTO
{
    public interface ITaskMapper
    {
        ITaskDto TransferToDto(ITaskModel taskModel, Dictionary<object, object>? mappedObjects = null);
        ITaskCreationInputDto TransferToDto(ITaskCreationInputVM taskCreationInputVM);
        ITaskCreationOutputVM TransferToTaskCreationOutputVM(int todoListId, string userId, string todoListTitle);
        ITaskDetailsOutputVM TransferToTaskDetailsVM(ITaskDto taskDto);
        ITaskModel TransferToModel(ITaskCreationInputDto taskCreationInputDto);
        ITaskModel TransferToModel(ITaskDto taskDto);
    }
}