using Project_Main.Models.DTOs;

namespace Project_Main.Models.Outputs.ViewModels
{
    public class BoardAllOutputVM : IBoardAllOutputVM
    {
        public ICollection<ITodoListDto> TodoLists { get; set; } = new List<ITodoListDto>();
    }
}
