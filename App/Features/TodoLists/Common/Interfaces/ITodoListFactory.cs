using App.Common.Interfaces;
using App.Features.Tasks.Common.Models;
using App.Features.TodoLists.Common.Models;
using App.Features.TodoLists.Edit.Models;

namespace App.Features.TodoLists.Common.Interfaces;

public interface ITodoListFactory : IBaseEntityFactory<TodoListModel, TodoListDto>
{
	TodoListEditInputDto CreateEditInputDto();
	TodoListModel CreateModel(TodoListModel originModel, ICollection<TaskModel> originTasks);
}
