using App.Common.ViewModels;
using App.Features.Tasks.Delete.Models;
using MediatR;

namespace App.Features.Tasks.Delete;

public class DeleteTaskQuery : IRequest<DeleteTaskQueryResponse>
{
    public Guid TodoListId { get; }
    public Guid TaskId { get; }

    public DeleteTaskQuery(Guid todoListId, Guid taskId)
    {
        TodoListId = todoListId;
        TaskId = taskId;
    }
}

public record DeleteTaskQueryResponse(
    WrapperViewModel<TaskDeleteInputVM, TaskDeleteOutputVM>? Data, 
    string? ErrorMessage = null, 
    int StatusCode = StatusCodes.Status200OK
){}
