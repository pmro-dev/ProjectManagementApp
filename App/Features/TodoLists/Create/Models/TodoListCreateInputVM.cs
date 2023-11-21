using App.Features.TodoLists.Create.Interfaces;

namespace App.Features.TodoLists.Create.Models;

public class TodoListCreateInputVM : ITodoListCreateInputVM
{
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}