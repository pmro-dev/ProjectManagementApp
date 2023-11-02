using Project_Main.Infrastructure.DTOs.Entities;

namespace Project_Main.Models.ViewModels.OutputModels
{
    public class BoardAllOutputVM : IBoardAllOutputVM
    {
        public IEnumerable<ITodoListDto> TodoLists { get; set; } = new List<ITodoListDto>();
    }
}
