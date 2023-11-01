using Project_Main.Infrastructure.DTOs;

namespace Project_Main.Models.ViewModels.OutputModels
{
    public interface IBoardsAllOutputVM
    {
        IEnumerable<ITodoListDto> TodoLists { get; set; }
    }
}