using App.Common;
using App.Features.Tasks.Delete.Models;
using MediatR;

namespace App.Features.Tasks.Delete;

public class DeleteTaskCommand : IRequest<DeleteTaskCommandResponse>
{
    public TaskDeleteInputVM InputVM { get; }

    public DeleteTaskCommand(TaskDeleteInputVM inputVM)
    {
        InputVM = inputVM;
    }
}

public record DeleteTaskCommandResponse(
    CustomRouteValues? Data, 
    string? ErrorMessage = null, 
    int StatusCode = StatusCodes.Status200OK
){}
