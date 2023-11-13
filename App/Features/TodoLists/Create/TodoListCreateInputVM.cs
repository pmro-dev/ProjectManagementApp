using App.Features.TodoLists.Create.Interfaces;

namespace App.Features.TodoLists.Create;

public class TodoListCreateInputVM : ITodoListCreateInputVM
{
	public string UserId { get; set; } = string.Empty;
	public string Title { get; set; } = string.Empty;
}