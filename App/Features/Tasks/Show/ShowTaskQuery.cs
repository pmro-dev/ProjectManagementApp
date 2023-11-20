using App.Features.Tasks.Show.Interfaces;
using MediatR;

namespace App.Features.Tasks.Show;

public class ShowTaskQuery : IRequest<IShowTaskOutputVM>
{
	public int TodoListId {  get; } 
	public int TaskId {  get; }

	public ShowTaskQuery(int todoListId, int taskId)
    {
		TodoListId = todoListId;
		TaskId = taskId;
	}
}
