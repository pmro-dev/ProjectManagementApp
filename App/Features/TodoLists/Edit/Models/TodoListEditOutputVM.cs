using App.Features.TodoLists.Edit.Interfaces;

namespace App.Features.TodoLists.Edit.Models;

public class TodoListEditOutputVM : ITodoListEditOutputVM
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}