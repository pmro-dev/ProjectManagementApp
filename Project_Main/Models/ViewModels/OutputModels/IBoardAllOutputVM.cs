using Project_Main.Infrastructure.DTOs.Entities;

namespace Project_Main.Models.ViewModels.OutputModels
{
    public interface IBoardAllOutputVM : IBoardViewModel
    {
        IEnumerable<ITodoListDto> TodoLists { get; set; }
    }
}