using App.Features.TodoLists.Edit.Interfaces;

namespace App.Features.TodoLists.Edit;

public class TodoListEditInputDto : ITodoListEditInputDto
{
	public string Title { get; set; } = string.Empty;
}