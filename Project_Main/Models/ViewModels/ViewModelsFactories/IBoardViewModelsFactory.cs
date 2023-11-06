using Project_Main.Infrastructure.DTOs.Entities;
using Project_Main.Models.ViewModels.OutputModels;

namespace Project_Main.Models.ViewModels.ViewModelsFactories
{
    public interface IBoardViewModelsFactory
    {
        public IBoardBrieflyOutputVM CreateBrieflyOutputVM(ICollection<ITodoListDto> todolistDto);
        public IBoardAllOutputVM CreateAllOutputVM(ICollection<ITodoListDto> todolistDto);
    }
}