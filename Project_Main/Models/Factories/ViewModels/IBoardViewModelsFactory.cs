using Project_Main.Models.DTOs;
using Project_Main.Models.Outputs.ViewModels;

namespace Project_Main.Models.Factories.ViewModels
{
    public interface IBoardViewModelsFactory
    {
        public IBoardBrieflyOutputVM CreateBrieflyOutputVM(ICollection<ITodoListDto> todolistDto);
        public IBoardAllOutputVM CreateAllOutputVM(ICollection<ITodoListDto> todolistDto);
    }
}