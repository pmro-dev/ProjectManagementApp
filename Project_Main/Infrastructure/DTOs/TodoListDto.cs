using Project_DTO;

namespace Project_Main.Infrastructure.DTOs
{
    public class TodoListDto : ITodoListDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public IEnumerable<ITaskDto> Tasks { get; set; } = new List<ITaskDto>();
    }
}
