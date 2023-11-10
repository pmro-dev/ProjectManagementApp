using Application.DTOs.Entities;

namespace Web.ViewModels.Outputs.Abstract
{
	public interface IBoardAllOutputVM
	{
		ICollection<ITodoListDto> TodoLists { get; set; }
	}
}