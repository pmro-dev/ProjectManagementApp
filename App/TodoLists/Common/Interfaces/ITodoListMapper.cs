using Web.TodoLists.Create.Interfaces;
using Web.TodoLists.Edit.Interfaces;

namespace Web.TodoLists.Common.Interfaces
{
	public interface ITodoListMapper
	{
		ICollection<ITodoListDto> TransferToDto(ICollection<TodoListModel> todoLists);
		ITodoListDto TransferToDto(ITodoListModel todoListModel, IDictionary<object, object>? mappedObjects = null);
		ITodoListDto TransferToDto(ITodoListCreateInputVM createInputVM);
		ITodoListEditInputDto TransferToDto(ITodoListEditInputVM editInputVM);
		TodoListModel TransferToModel(ITodoListDto todoListDto, IDictionary<object, object>? mappedObjects = null);
		void UpdateModel(TodoListModel todoListDbModel, ITodoListEditInputDto taskEditInputDto);
	}
}