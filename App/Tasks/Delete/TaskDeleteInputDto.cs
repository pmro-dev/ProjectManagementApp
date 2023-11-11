using Web.Tasks.Delete.Interfaces;

namespace Web.Tasks.Delete;

public class TaskDeleteInputDto : ITaskDeleteInputDto
{
	public int Id { get; set; }
	public int TodoListId { get; set; }
}
