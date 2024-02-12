namespace App.Features.Tasks.Delete.Interfaces;

public interface ITaskDeleteInputDto
{
	Guid Id { get; set; }
	Guid TodoListId { get; set; }
}