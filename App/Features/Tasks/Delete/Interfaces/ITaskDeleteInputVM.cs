namespace App.Features.Tasks.Delete.Interfaces;

public interface ITaskDeleteInputVM
{
	Guid Id { get; set; }
	Guid TodoListId { get; set; }
}