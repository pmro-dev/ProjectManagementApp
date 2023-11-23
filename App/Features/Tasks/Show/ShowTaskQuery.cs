using App.Features.Tasks.Show.Models;
using MediatR;

namespace App.Features.Tasks.Show;

public class ShowTaskQuery : IRequest<ShowTaskQueryResponse>
{
	public int TodoListId {  get; } 
	public int TaskId {  get; }

	public ShowTaskQuery(int todoListId, int taskId)
    {
		TodoListId = todoListId;
		TaskId = taskId;
	}
}

public record ShowTaskQueryResponse(ShowTaskOutputVM? Data, string? ErrorMessage = null, int StatusCode = StatusCodes.Status200OK) { }

