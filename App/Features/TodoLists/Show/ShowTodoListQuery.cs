using MediatR;

namespace App.Features.TodoLists.Show;

public class ShowTodoListQuery : IRequest<TodoListDetailsOutputVM>
{
	public int TodoListId { get; }
	public DateTime? FilterDueDate { get; }

	public ShowTodoListQuery(int todoListId, DateTime? filterDueDate)
	{
		TodoListId = todoListId;
		FilterDueDate = filterDueDate;
	}
}
