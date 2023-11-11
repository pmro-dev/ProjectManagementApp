using Web.TodoLists.Create.Interfaces;

namespace Web.TodoLists.Create;

public class TodoListCreateInputVM : ITodoListCreateInputVM
{
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}