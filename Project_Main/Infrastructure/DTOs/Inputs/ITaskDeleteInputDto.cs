namespace Project_Main.Infrastructure.DTOs.Inputs
{
    public interface ITaskDeleteInputDto
    {
        int Id { get; set; }
        int TodoListId { get; set; }
    }
}