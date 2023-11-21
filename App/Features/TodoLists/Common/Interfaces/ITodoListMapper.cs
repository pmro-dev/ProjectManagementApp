using App.Features.TodoLists.Common.Models;
using App.Features.TodoLists.Create.Models;
using App.Features.TodoLists.Edit.Models;

namespace App.Features.TodoLists.Common.Interfaces;

public interface ITodoListMapper
{
	ICollection<TodoListDto> TransferToDto(ICollection<TodoListModel> todoLists);
	TodoListDto TransferToDto(TodoListModel todoListModel, IDictionary<object, object>? mappedObjects = null);
	TodoListDto TransferToDto(TodoListCreateInputVM createInputVM);
	TodoListEditInputDto TransferToDto(TodoListEditInputVM editInputVM);
	TodoListModel TransferToModel(TodoListDto todoListDto, IDictionary<object, object>? mappedObjects = null);
	void UpdateModel(TodoListModel todoListDbModel, TodoListEditInputDto taskEditInputDto);
}