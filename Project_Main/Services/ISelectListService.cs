using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Main.Models.DTOs;

namespace Project_Main.Services
{
	public interface ISelectListService
	{
		SelectList CreateTodoListSelector(ICollection<ITodoListDto> userTodoListDtos, int defaultSelectedTodoListId);
		SelectList CreateTaskStatusSelector(ITaskDto taskDto);
	}
}