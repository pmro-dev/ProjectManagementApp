using App.Common.ViewModels;
using App.Features.Tasks.Create.Models;
using MediatR;

namespace App.Features.Tasks.Create;

public class CreateTaskQuery : IRequest<CreateTaskQueryResponse>
{
    public Guid TodoListId { get; }

    public CreateTaskQuery(Guid todoListId)
    {
        TodoListId = todoListId;
    }
}

public record CreateTaskQueryResponse(
    WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM>? Data, 
    string? ErrorMessage = null, 
    int StatusCode = StatusCodes.Status200OK
){}
