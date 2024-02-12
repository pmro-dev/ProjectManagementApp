using App.Features.Tasks.Delete.Interfaces;

namespace App.Features.Tasks.Delete.Models;

public class TaskDeleteInputDto : ITaskDeleteInputDto
{
    public Guid Id { get; set; }
    public Guid TodoListId { get; set; }
}
