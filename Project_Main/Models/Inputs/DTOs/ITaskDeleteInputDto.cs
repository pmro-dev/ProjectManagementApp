namespace Project_Main.Models.Inputs.DTOs
{
    public interface ITaskDeleteInputDto
    {
        int Id { get; set; }
        int TodoListId { get; set; }
    }
}