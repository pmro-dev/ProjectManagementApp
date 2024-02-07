namespace App.Features.Tasks.Create.Interfaces;

public interface ITaskCreateOutputVM
{
	Guid TodoListId { get; set; }
	string TodoListName { get; set; }
	string UserId { get; set; }
}