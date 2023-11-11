using Web.TodoLists.Create.Interfaces;

namespace Web.TodoLists.Create;

public class TodoListCreateOutputVM : ITodoListCreateOutputVM
{
    public string UserId { get; set; } = string.Empty;
}
