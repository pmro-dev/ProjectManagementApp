using App.Features.Tasks.Show.Models;
using MediatR;

namespace App.Features.Tasks.Show;

public class ShowTaskQuery : IRequest<ShowTaskQueryResponse>
{
	public Guid TodoListId {  get; } 
	public Guid TaskId {  get; }

	public ShowTaskQuery(Guid todoListId, Guid taskId)
    {
		TodoListId = todoListId;
		TaskId = taskId;
	}
}

public record ShowTaskQueryResponse(
	ShowTaskOutputVM? Data, 
	string? ErrorMessage = null, 
	int StatusCode = StatusCodes.Status200OK
){}

