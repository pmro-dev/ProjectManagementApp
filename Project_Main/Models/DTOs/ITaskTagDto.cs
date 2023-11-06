namespace Project_Main.Models.DTOs
{
    public interface ITaskTagDto
    {
        ITagDto Tag { get; set; }

        int TagId { get; set; }

        ITaskDto Task { get; set; }

        int TaskId { get; set; }
    }
}