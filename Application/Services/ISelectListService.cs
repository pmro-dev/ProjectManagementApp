using Application.DTOs.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Application.Services;

public interface ISelectListService
{
	SelectList CreateTodoListSelector(ICollection<ITodoListDto> userTodoListDtos, int defaultSelectedTodoListId);
	SelectList CreateTaskStatusSelector(ITaskDto taskDto);
}