using Project_DTO;

namespace Project_Main.Infrastructure.DTOs
{
    public class TaskTagDto : ITaskTagDto
    {
        public int TaskId { get; set; }
        public ITaskDto Task { get; set; } = new TaskDto();

        public int TagId { get; set; }
        public ITagDto Tag { get; set; } = new TagDto();
    }
}
