namespace Application.DTOs.Entities
{
    public interface ITagDto
    {
        int Id { get; set; }
        ICollection<ITaskTagDto> TaskTags { get; set; }
        string Title { get; set; }
    }
}