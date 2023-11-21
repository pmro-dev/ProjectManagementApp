using App.Features.Tasks.Create.Models;
using MediatR;

namespace App.Features.Tasks.Create;


//TODO create specific result type with routeValue object etc...
public class CreateTaskCommand : IRequest<object>
{
    public int TodoListId { get; }

    public TaskCreateInputVM TaskCreateInputVM { get; }

    public CreateTaskCommand(TaskCreateInputVM taskCreateInputVM, int todoListId)
    {
        TodoListId = todoListId;
        TaskCreateInputVM = taskCreateInputVM;
    }
}
