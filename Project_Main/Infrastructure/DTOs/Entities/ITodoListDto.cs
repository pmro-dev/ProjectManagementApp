namespace Project_Main.Infrastructure.DTOs.Entities
{
    public interface ITodoListDto
    {
        int Id { get; set; }
        IEnumerable<ITaskDto> Tasks { get; set; }
        string Title { get; set; }
        string UserId { get; set; }
    }
}