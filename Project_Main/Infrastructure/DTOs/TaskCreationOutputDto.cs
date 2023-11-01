using Project_Main.Services.DTO.ViewModelsFactories;

namespace Project_Main.Infrastructure.DTOs
{
    public class TaskCreationOutputDto : ITaskCreationOutputDto
    {
        public int TodoListId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string TodoListName { get; set; } = string.Empty;
    }
}
