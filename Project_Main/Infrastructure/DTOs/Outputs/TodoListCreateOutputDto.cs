using Project_Main.Infrastructure.DTOs.Entities;

namespace Project_Main.Infrastructure.DTOs.Outputs
{
    public class TodoListCreateOutputDto : ITodoListCreateOutputDto
    {
        public ITodoListDto TodoListDto { get; set; } = new TodoListDto();
    }
}
