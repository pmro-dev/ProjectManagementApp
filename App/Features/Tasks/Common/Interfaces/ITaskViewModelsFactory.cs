#region USINGS

using App.Common.ViewModels;
using App.Features.Tasks.Common.Models;
using App.Features.Tasks.Create.Models;
using App.Features.Tasks.Delete.Models;
using App.Features.Tasks.Edit.Models;
using App.Features.Tasks.Show.Models;
using App.Features.TodoLists.Common.Models;

#endregion

namespace App.Features.Tasks.Common.Interfaces;

public interface ITaskViewModelsFactory
{
	public ShowTaskOutputVM CreateDetailsOutputVM(TaskDto taskDto);
	public TaskCreateOutputVM CreateCreateOutputVM(TodoListDto todoListDto);
	public TaskCreateInputVM CreateCreateInputVM(TaskDto taskDto);
	public WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM> CreateWrapperCreateVM();
	public WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM> CreateWrapperCreateVM(TaskDto taskDto, TodoListDto todoListDto);
	public WrapperViewModel<TaskEditInputVM, TaskEditOutputVM> CreateWrapperEditVM();
	public WrapperViewModel<TaskDeleteInputVM, TaskDeleteOutputVM> CreateWrapperDeleteVM();
	public TaskEditInputVM CreateEditInputVM(TaskDto taskDto);
	public TaskEditOutputVM CreateEditOutputVM(TaskDto taskDto, ICollection<TodoListDto> userTodoListDtos);
	public TaskDeleteOutputVM CreateDeleteOutputVM(TaskDto taskDto);
	public TaskDeleteInputVM CreateDeleteInputVM(int id, int todoListId);
}
