using App.Features.Tasks.Delete.Interfaces;

namespace App.Features.Tasks.Delete.Models;

public class TaskDeleteInputVM : ITaskDeleteInputVM
{
	public Guid Id { get; set; }
	public Guid TodoListId { get; set; }
}
