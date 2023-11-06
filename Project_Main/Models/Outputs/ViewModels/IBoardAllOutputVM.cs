using Project_Main.Models.DTOs;

namespace Project_Main.Models.Outputs.ViewModels
{
    public interface IBoardAllOutputVM
    {
        ICollection<ITodoListDto> TodoLists { get; set; }
    }
}