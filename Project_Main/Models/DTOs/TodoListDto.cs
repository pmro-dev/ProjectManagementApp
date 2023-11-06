namespace Project_Main.Models.DTOs
{
    public class TodoListDto : ITodoListDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public ICollection<ITaskDto> Tasks { get; set; } = new List<ITaskDto>();
    }
}
