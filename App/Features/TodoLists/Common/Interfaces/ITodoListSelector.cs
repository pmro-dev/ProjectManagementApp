using App.Features.TodoLists.Common.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Features.TodoLists.Common.Interfaces;

public interface ITodoListSelector
{
	SelectList Create(ICollection<TodoListDto> userTodoListDtos, int defaultSelectedTodoListId);
}
