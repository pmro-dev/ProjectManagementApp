using Project_Main.Infrastructure.DTOs.Entities;
using Project_Main.Models.ViewModels.OutputModels;

namespace Project_Main.Services.DTO.ViewModelsFactories
{
    public interface IBoardViewModelsFactory
    {
        public IBoardBrieflyOutputVM CreateBrieflyOutputVM(ICollection<ITodoListDto> todolistDto);
        public IBoardAllOutputVM CreateAllOutputVM(ICollection<ITodoListDto> todolistDto);
    }
}