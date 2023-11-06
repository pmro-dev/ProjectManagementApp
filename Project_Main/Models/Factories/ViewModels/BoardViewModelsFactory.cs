using Project_Main.Models.DTOs;
using Project_Main.Models.Outputs.ViewModels;

namespace Project_Main.Models.Factories.ViewModels
{
    public class BoardViewModelsFactory : IBoardViewModelsFactory
    {
        public IBoardAllOutputVM CreateAllOutputVM(ICollection<ITodoListDto> todolistDto)
        {
            return new BoardAllOutputVM()
            {
                TodoLists = todolistDto
            };
        }

        public IBoardBrieflyOutputVM CreateBrieflyOutputVM(ICollection<ITodoListDto> todolistDto)
        {
            return new BoardBrieflyOutputVM()
            {
                TodoLists = todolistDto
            };
        }
    }
}
