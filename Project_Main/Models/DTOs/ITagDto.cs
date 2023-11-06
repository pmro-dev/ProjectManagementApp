namespace Project_Main.Models.DTOs
{
    public interface ITagDto
    {
        int Id { get; set; }
        ICollection<ITaskTagDto> TaskTags { get; set; }
        string Title { get; set; }
    }
}