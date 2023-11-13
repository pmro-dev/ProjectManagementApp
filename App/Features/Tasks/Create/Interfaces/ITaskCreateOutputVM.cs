namespace App.Features.Tasks.Create.Interfaces;

public interface ITaskCreateOutputVM
{
	int TodoListId { get; set; }
	string TodoListName { get; set; }
	string UserId { get; set; }
}