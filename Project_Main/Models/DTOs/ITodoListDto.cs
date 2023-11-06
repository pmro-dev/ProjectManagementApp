namespace Project_Main.Models.DTOs
{
    public interface ITodoListDto
    {
        int Id { get; set; }
        ICollection<ITaskDto> Tasks { get; set; }
        string Title { get; set; }
        string UserId { get; set; }
    }
}