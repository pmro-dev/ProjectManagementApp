using Web.Tasks.Common.Interfaces;
using Web.TodoLists.Common.Interfaces;

namespace Web.TodoLists.Common
{
	public class TodoListDto : ITodoListDto
	{
		public int Id { get; set; }

		public string Title { get; set; } = string.Empty;

		public string UserId { get; set; } = string.Empty;

		public ICollection<ITaskDto> Tasks { get; set; } = new List<ITaskDto>();
	}
}
