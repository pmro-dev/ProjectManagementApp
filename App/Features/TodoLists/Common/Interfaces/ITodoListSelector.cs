using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Features.TodoLists.Common.Interfaces;

public interface ITodoListSelector
{
	SelectList Create(ICollection<ITodoListDto> userTodoListDtos, int defaultSelectedTodoListId);
}
