using Web.TodoLists.Edit.Interfaces;

namespace Web.TodoLists.Edit;

public class TodoListEditInputDto : ITodoListEditInputDto
{
	public string Title { get; set; } = string.Empty;
}