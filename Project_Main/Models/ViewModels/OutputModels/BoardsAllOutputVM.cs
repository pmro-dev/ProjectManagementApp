using Project_Main.Infrastructure.DTOs;

namespace Project_Main.Models.ViewModels.OutputModels
{
    public class BoardsAllOutputVM
    {
        public IEnumerable<TodoListModelDto> TodoListDtos { get; set; } = new List<TodoListModelDto>();
    }
}
