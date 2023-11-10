using Application.DTOs.Entities;
using Application.DTOs.ForViewModels.Inputs;
using Domain.Entities;

namespace Application.Factories.DTOs;

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
