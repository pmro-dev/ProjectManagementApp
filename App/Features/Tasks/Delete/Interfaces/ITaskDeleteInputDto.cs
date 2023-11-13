namespace App.Features.Tasks.Delete.Interfaces;

public interface ITaskDeleteInputDto
{
	int Id { get; set; }
	int TodoListId { get; set; }
}