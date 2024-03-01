using App.Features.Projects.Show.TodoListsBoard.ViewModels;
using MediatR;
using System.Linq.Expressions;

namespace App.Features.Projects.Show.TodoListsBoard;

public class ShowProjectTodoListsQuery : IRequest<ShowProjectTodoListsQueryResponse>
{
    public Guid ProjectId { get; }
    public int PageNumber { get; }
    public int ItemsPerPageCount { get; }

    // Here is specified selector for sorting to do lists by progress made but in the final version, user should choose: sort by the name, best or worst progress... 
    public Expression<Func<TodoListsBoardTodoListVM, object>> OrderBySelector { get; }

    public ShowProjectTodoListsQuery(Guid projectId,
        Expression<Func<TodoListsBoardTodoListVM, object>> orderBySelector,
        int pageNumber, int itemsPerPageCount)
    {
        ProjectId = projectId;
        OrderBySelector = orderBySelector;
        ItemsPerPageCount = itemsPerPageCount;
        PageNumber = pageNumber;
    }
}

public record ShowProjectTodoListsQueryResponse(
    ProjectBoardTodoListsOutputVM? Data,
    string? ErrorMessage = null,
    int StatusCode = StatusCodes.Status200OK
)
{ }
