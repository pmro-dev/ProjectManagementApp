using Project_DomainEntities;
using Project_Main.Infrastructure.DTOs.Entities;
using Project_Main.Infrastructure.DTOs.Inputs;
using Project_Main.Models.ViewModels.InputModels;

namespace Project_Main.Services.DTO
{
    public interface ITodoListMapper
    {
		ICollection<ITodoListDto> TransferToDto(ICollection<ITodoListModel> todoLists);
        ITodoListDto TransferToDto(ITodoListModel todoListModel, Dictionary<object, object>? mappedObjects = null);
        ITodoListDto TransferToDto(ITodoListCreateInputVM createInputVM);
        ITodoListEditInputDto TransferToDto(ITodoListEditInputVM editInputVM);
        ITodoListModel TransferToModel(ITodoListDto todoListDto, Dictionary<object, object>? mappedObjects = null);
        void UpdateModel(ITodoListModel todoListDbModel, ITodoListEditInputDto taskEditInputDto);
    }
}