using App.Features.TodoLists.Edit.Interfaces;

namespace App.Features.TodoLists.Edit.Models;

public class TodoListEditInputVM : ITodoListEditInputVM
{
    public string Title { get; set; } = string.Empty;
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
}