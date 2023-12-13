using App.Common;
using App.Features.Tasks.Create.Models;
using MediatR;

namespace App.Features.Tasks.Create;

public class CreateTaskCommand : IRequest<CreateTaskCommandResponse>
{
    public int TodoListId { get; }

    public TaskCreateInputVM InputVM { get; }

    public CreateTaskCommand(TaskCreateInputVM inputVM, int todoListId)
    {
        TodoListId = todoListId;
        InputVM = inputVM;
    }
}

public record CreateTaskCommandResponse(
    CustomRouteValues? Data = null, 
    string? ErrorMessage = null, 
    int StatusCode = StatusCodes.Status201Created
){}

