namespace Project_Main.Infrastructure.DTOs.Entities
{
    public class TagDto : ITagDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public IEnumerable<ITaskTagDto> TaskTags { get; set; } = new List<ITaskTagDto>();
    }
}
