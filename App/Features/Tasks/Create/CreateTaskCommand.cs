using App.Common;
using App.Features.Tasks.Create.Models;
using MediatR;

namespace App.Features.Tasks.Create;

public class CreateTaskCommand : IRequest<CreateTaskCommandResponse>
{
    public int TodoListId { get; }

    public TaskCreateInputVM TaskCreateInputVM { get; }

    public CreateTaskCommand(TaskCreateInputVM taskCreateInputVM, int todoListId)
    {
        TodoListId = todoListId;
        TaskCreateInputVM = taskCreateInputVM;
    }
}

public record CreateTaskCommandResponse(CustomRouteValues? Data, string? ErrorMessage = null, int StatusCode = StatusCodes.Status201Created) { }

