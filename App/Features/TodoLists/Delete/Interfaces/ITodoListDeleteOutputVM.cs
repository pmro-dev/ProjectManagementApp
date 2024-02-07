namespace App.Features.TodoLists.Delete.Interfaces;

public interface ITodoListDeleteOutputVM
{
	public Guid Id { get; set; }
	public int TasksCount { get; set; }
	public string Title { get; set; }
}