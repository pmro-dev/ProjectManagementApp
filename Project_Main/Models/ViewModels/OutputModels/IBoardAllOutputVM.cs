using Project_Main.Infrastructure.DTOs.Entities;

namespace Project_Main.Models.ViewModels.OutputModels
{
    public interface IBoardAllOutputVM
    {
		ICollection<ITodoListDto> TodoLists { get; set; }
    }
}