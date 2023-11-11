using Web.Common.Interfaces;
using Web.TodoLists.Edit;

namespace Web.TodoLists.Common.Interfaces;

public interface ITodoListFactory : IBaseEntityFactory<TodoListModel, TodoListDto>
{
	TodoListEditInputDto CreateEditInputDto();
}
