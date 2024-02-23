using App.Features.Projects.Show.ProjectManagerBoards.ProjectBoard;
using App.Features.Projects.Show.ProjectManagerBoards.TodoListsBoard;
using MediatR;
using System.Linq.Expressions;

namespace App.Features.Projects.Show;

public class ShowProjectTodoListsBoardQuery : IRequest<ShowProjectTodoListsBoardQueryResponse>
{
	public Guid ProjectId { get; }
	public int PageNumber { get; }
	public int ItemsPerPageCount { get; }

	// Here is specified selector for sorting to do lists by progress made but in the final version, user should choose: sort by the name, best or worst progress... 
	public Expression<Func<TodoListsBoardTodoListVM, object>> OrderBySelector { get; }

	public ShowProjectTodoListsBoardQuery(Guid projectId, 
		Expression<Func<TodoListsBoardTodoListVM, object>> orderBySelector,
		int pageNumber, int itemsPerPageCount)
	{
		ProjectId = projectId;
		OrderBySelector = orderBySelector;
		ItemsPerPageCount = itemsPerPageCount;
		PageNumber = pageNumber;
	}
}

public record ShowProjectTodoListsBoardQueryResponse(
	ProjectBoardTodoListsOutputVM? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}
