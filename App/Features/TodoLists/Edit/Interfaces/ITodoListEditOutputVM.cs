namespace App.Features.TodoLists.Edit.Interfaces;

public interface ITodoListEditOutputVM
{
	Guid Id { get; set; }
	string Title { get; set; }
	string UserId { get; set; }
}