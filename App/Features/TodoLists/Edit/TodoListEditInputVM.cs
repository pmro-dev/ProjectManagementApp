using App.Features.TodoLists.Edit.Interfaces;

namespace App.Features.TodoLists.Edit;

public class TodoListEditInputVM : ITodoListEditInputVM
{
	public string Title { get; set; } = string.Empty;
}