using Web.TodoLists.Delete.Interfaces;

namespace Web.TodoLists.Delete;

/// <summary>
/// Model for deletion ToDoList.
/// </summary>
public class TodoListDeleteOutputVM : ITodoListDeleteOutputVM
{
    public int TasksCount { get; set; } = 0;
    public int Id { get; set; } = 0;
    public string Title { get; set; } = string.Empty;
}
