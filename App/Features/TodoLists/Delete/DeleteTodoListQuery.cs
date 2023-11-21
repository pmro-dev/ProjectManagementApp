using App.Features.TodoLists.Delete.Models;
using MediatR;

namespace App.Features.TodoLists.Delete;

public class DeleteTodoListQuery : IRequest<TodoListDeleteOutputVM>
{
	public int RouteTodoListId { get; }

	public DeleteTodoListQuery(int routeTodoListId)
	{
		RouteTodoListId = routeTodoListId;
	}
}
