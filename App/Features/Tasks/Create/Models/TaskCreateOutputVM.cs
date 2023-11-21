using App.Features.Tasks.Create.Interfaces;

namespace App.Features.Tasks.Create.Models;

public class TaskCreateOutputVM : ITaskCreateOutputVM
{
    public int TodoListId { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string TodoListName { get; set; } = string.Empty;
}
