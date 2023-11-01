namespace Project_Main.Infrastructure.DTOs
{
    public interface ITagDto
    {
        int Id { get; set; }
        IEnumerable<ITaskTagDto> TaskTags { get; set; }
        string Title { get; set; }
    }
}