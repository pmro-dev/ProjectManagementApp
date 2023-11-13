namespace App.Features.TodoLists.Create.Interfaces;

public interface ITodoListCreateInputVM
{
	string UserId { get; set; }
	string Title { get; set; }
}