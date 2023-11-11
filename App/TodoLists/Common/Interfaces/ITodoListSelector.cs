using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.TodoLists.Common.Interfaces
{
    public interface ITodoListSelector
    {
        SelectList Create(ICollection<ITodoListDto> userTodoListDtos, int defaultSelectedTodoListId);
    }
}
