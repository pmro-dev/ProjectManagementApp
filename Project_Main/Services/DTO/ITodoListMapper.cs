using Project_DomainEntities;
using Project_Main.Infrastructure.DTOs;
using Project_Main.Models.ViewModels.OutputModels;

namespace Project_Main.Services.DTO
{
    public interface ITodoListMapper
    {
        BoardsAllOutputVM TransferToBoardsAllOutputVM(IEnumerable<ITodoListDto> todoListDtos);
        BoardsBrieflyOutputVM TransferToBoardsBrieflyOutputVM(IEnumerable<ITodoListDto> todoListsDtos);
        IEnumerable<ITodoListDto> TransferToDto(IEnumerable<ITodoListModel> todoLists);
        ITodoListDto TransferToDto(ITodoListModel todoListModel, Dictionary<object, object>? mappedObjects = null);
        ITodoListModel TransferToModel(ITodoListDto todoListDto);
        BoardsSingleDetailsOutputVM TransferToSingleDetailsOutputVM(ITodoListDto todoListDto, DateTime? filterDueDate = null);
    }
}