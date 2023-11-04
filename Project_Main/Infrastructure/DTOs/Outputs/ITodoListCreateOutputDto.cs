using Project_Main.Infrastructure.DTOs.Entities;

namespace Project_Main.Infrastructure.DTOs.Outputs
{
    public interface ITodoListCreateOutputDto
    {
        public ITodoListDto TodoListDto { get; set; }
    }
}
