using Application.DTOs.Entities;
using Web.ViewModels.Outputs.Abstract;

namespace Web.ViewModels.Outputs
{
	public class BoardAllOutputVM : IBoardAllOutputVM
	{
		public ICollection<ITodoListDto> TodoLists { get; set; } = new List<ITodoListDto>();
	}
}
