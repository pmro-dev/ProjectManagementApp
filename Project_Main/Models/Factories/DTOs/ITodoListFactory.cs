using Project_DomainEntities;
using Project_Main.Models.DTOs;
using Project_Main.Models.Inputs.DTOs;

namespace Project_Main.Models.Factories.DTOs
{
	public interface ITodoListFactory : IBaseEntityFactory<TodoListModel, TodoListDto>
	{
		TodoListEditInputDto CreateEditInputDto();
	}
}
