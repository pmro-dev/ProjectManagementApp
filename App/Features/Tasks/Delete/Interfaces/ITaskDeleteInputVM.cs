namespace App.Features.Tasks.Delete.Interfaces;

public interface ITaskDeleteInputVM
{
	int Id { get; set; }
	int TodoListId { get; set; }
}