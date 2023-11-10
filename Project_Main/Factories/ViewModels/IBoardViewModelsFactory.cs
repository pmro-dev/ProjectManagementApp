using Application.DTOs.Entities;
using Web.ViewModels.Outputs.Abstract;

namespace Web.Factories.ViewModels;

public interface IBoardViewModelsFactory
{
	public IBoardBrieflyOutputVM CreateBrieflyOutputVM(ICollection<ITodoListDto> todolistDto);
	public IBoardAllOutputVM CreateAllOutputVM(ICollection<ITodoListDto> todolistDto);
}