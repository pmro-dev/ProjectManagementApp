using Web.TodoLists.Common.Interfaces;
using Web.TodoLists.Edit;

namespace Web.TodoLists.Common;

public class TodoListFactory : ITodoListFactory
{
	public TodoListModel CreateModel()
	{
		return new TodoListModel();
	}

	public TodoListDto CreateDto()
	{
		return new TodoListDto();
	}

	public TodoListEditInputDto CreateEditInputDto()
	{
		return new TodoListEditInputDto();
	}
}
