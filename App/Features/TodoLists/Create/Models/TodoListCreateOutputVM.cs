using App.Features.TodoLists.Create.Interfaces;

namespace App.Features.TodoLists.Create.Models;

public class TodoListCreateOutputVM : ITodoListCreateOutputVM
{
    public string UserId { get; set; } = string.Empty;
}
