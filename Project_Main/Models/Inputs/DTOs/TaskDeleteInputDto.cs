namespace Project_Main.Models.Inputs.DTOs
{
    public class TaskDeleteInputDto : ITaskDeleteInputDto
    {
        public int Id { get; set; }
        public int TodoListId { get; set; }
    }
}
