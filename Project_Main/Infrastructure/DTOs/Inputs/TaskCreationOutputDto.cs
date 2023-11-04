using Project_Main.Infrastructure.DTOs.Outputs;

namespace Project_Main.Infrastructure.DTOs.Inputs
{
    public class TaskCreationOutputDto : ITaskCreateOutputDto
    {
        public int TodoListId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string TodoListName { get; set; } = string.Empty;
    }
}
