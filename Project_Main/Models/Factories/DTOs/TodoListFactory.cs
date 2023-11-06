using Project_DomainEntities;
using Project_Main.Models.DTOs;
using Project_Main.Models.Inputs.DTOs;

namespace Project_Main.Models.Factories.DTOs
{
	public class TodoListFactory : ITodoListFactory
	{
		public TodoListModel CreateModel()
		{
			return new TodoListModel();
		}

		public TodoListDto CreateDto()
		{
			return new TodoListDto();
		}

		public TodoListEditInputDto CreateEditInputDto()
		{
			return new TodoListEditInputDto();
		}
	}
}
