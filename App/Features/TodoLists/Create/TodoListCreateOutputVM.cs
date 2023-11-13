using App.Features.TodoLists.Create.Interfaces;

namespace App.Features.TodoLists.Create;

public class TodoListCreateOutputVM : ITodoListCreateOutputVM
{
	public string UserId { get; set; } = string.Empty;
}
