using Project_DTO;

namespace Project_Main.Infrastructure.DTOs
{
	public class TodoListModelDto
	{
		public int Id { get; set; }

		public string Title { get; set; } = string.Empty;

		public string UserId { get; set; } = string.Empty;

		public IEnumerable<TaskModelDto> Tasks { get; set; } = new List<TaskModelDto>();
	}
}
