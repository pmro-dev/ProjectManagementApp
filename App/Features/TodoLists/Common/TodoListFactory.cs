using App.Features.Tasks.Common.Models;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.TodoLists.Common.Models;
using App.Features.TodoLists.Edit.Models;

namespace App.Features.TodoLists.Common;

public class TodoListFactory : ITodoListFactory
{
	public TodoListModel CreateModel()
	{
		return new TodoListModel();
	}
	public TodoListModel CreateModel(TodoListModel originModel, ICollection<TaskModel> originTasks)
	{
		return new TodoListModel()
		{
			Title = originModel.Title + "###",
			Tasks = originTasks,
			UserId = originModel.UserId
		};
	}

	public TodoListDto CreateDto()
	{
		return new TodoListDto();
	}

	public TodoListEditInputDto CreateEditInputDto()
	{
		return new TodoListEditInputDto();
	}
}
