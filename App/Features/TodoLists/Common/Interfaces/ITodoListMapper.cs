using App.Features.TodoLists.Common.Models;
using App.Features.TodoLists.Create.Interfaces;
using App.Features.TodoLists.Edit.Interfaces;

namespace App.Features.TodoLists.Common.Interfaces;

public interface ITodoListMapper
{
	ICollection<ITodoListDto> TransferToDto(ICollection<TodoListModel> todoLists);
	ITodoListDto TransferToDto(ITodoListModel todoListModel, IDictionary<object, object>? mappedObjects = null);
	ITodoListDto TransferToDto(ITodoListCreateInputVM createInputVM);
	ITodoListEditInputDto TransferToDto(ITodoListEditInputVM editInputVM);
	TodoListModel TransferToModel(ITodoListDto todoListDto, IDictionary<object, object>? mappedObjects = null);
	void UpdateModel(TodoListModel todoListDbModel, ITodoListEditInputDto taskEditInputDto);
}