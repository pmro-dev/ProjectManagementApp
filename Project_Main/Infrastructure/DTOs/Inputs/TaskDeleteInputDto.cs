namespace Project_Main.Infrastructure.DTOs.Inputs
{
    public class TaskDeleteInputDto : ITaskDeleteInputDto
    {
        public int Id { get; set; }
        public int TodoListId { get; set; }
    }
}
