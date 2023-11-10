using Application.DTOs.Entities;
using Application.DTOs.ForViewModels.Inputs;
using Domain.Entities;

namespace Application.Factories.DTOs;

public interface ITodoListFactory : IBaseEntityFactory<TodoListModel, TodoListDto>
{
	TodoListEditInputDto CreateEditInputDto();
}
