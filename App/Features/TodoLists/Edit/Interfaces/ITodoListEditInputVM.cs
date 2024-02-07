namespace App.Features.TodoLists.Edit.Interfaces;

public interface ITodoListEditInputVM
{
	string Title { get; set; }
    public Guid Id { get; set; }
    public string UserId { get; set; }
}