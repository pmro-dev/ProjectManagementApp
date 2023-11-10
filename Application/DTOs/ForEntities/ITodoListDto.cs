namespace Application.DTOs.Entities
{
    public interface ITodoListDto
    {
        int Id { get; set; }
        ICollection<ITaskDto> Tasks { get; set; }
        string Title { get; set; }
        string UserId { get; set; }
    }
}