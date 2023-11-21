using App.Features.Tasks.Delete.Models;
using MediatR;

namespace App.Features.Tasks.Delete;

public class DeleteTaskCommand : IRequest<object>
{
    public TaskDeleteInputVM InputVM { get; }

    public DeleteTaskCommand(TaskDeleteInputVM inputVM)
    {
        InputVM = inputVM;
    }
}
