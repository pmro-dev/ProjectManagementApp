using Microsoft.AspNetCore.Mvc.Rendering;
using Web.TodoLists.Common.Interfaces;

namespace Web.TodoLists.Common
{
    public class TodoListSelector : ITodoListSelector
	{
		public SelectList Create(ICollection<ITodoListDto> userTodoListDtos, int defaultSelectedTodoListId)
		{
			SelectList todoListSelectorDto = new(userTodoListDtos, nameof(ITodoListDto.Id), nameof(ITodoListDto.Title), defaultSelectedTodoListId);

			return todoListSelectorDto;
		}
	}
}
