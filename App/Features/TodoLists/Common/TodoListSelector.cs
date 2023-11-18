using App.Features.TodoLists.Common.Interfaces;
using App.Features.TodoLists.Common.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Features.TodoLists.Common;

public class TodoListSelector : ITodoListSelector
{
	public SelectList Create(ICollection<TodoListDto> userTodoListDtos, int defaultSelectedTodoListId)
	{
		SelectList todoListSelectorDto = new(userTodoListDtos, nameof(TodoListDto.Id), nameof(TodoListDto.Title), defaultSelectedTodoListId);

		return todoListSelectorDto;
	}
}
