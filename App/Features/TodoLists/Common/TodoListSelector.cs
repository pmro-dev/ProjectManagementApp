using App.Features.TodoLists.Common.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Features.TodoLists.Common;

public class TodoListSelector : ITodoListSelector
{
	public SelectList Create(ICollection<ITodoListDto> userTodoListDtos, int defaultSelectedTodoListId)
	{
		SelectList todoListSelectorDto = new(userTodoListDtos, nameof(ITodoListDto.Id), nameof(ITodoListDto.Title), defaultSelectedTodoListId);

		return todoListSelectorDto;
	}
}
