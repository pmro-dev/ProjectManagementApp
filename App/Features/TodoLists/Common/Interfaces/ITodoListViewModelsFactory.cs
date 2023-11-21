using App.Common.ViewModels;
using App.Features.TodoLists.Common.Models;
using App.Features.TodoLists.Create.Models;
using App.Features.TodoLists.Delete.Models;
using App.Features.TodoLists.Edit.Models;
using App.Features.TodoLists.Show.Models;

namespace App.Features.TodoLists.Common.Interfaces;

public interface ITodoListViewModelsFactory
{
	TodoListCreateOutputVM CreateCreateOutputVM(string userId);
	TodoListCreateInputVM CreateCreateInputVM(TodoListDto todoListDto);
	TodoListEditOutputVM CreateEditOutputVM(TodoListDto todoListDto);
	TodoListEditInputVM CreateEditInputVM(TodoListDto todoListDto);
	TodoListDeleteOutputVM CreateDeleteOutputVM(TodoListDto todoListDto);
	TodoListDetailsOutputVM CreateDetailsOutputVM(TodoListDto todoListDto, DateTime? filterDueDate);
	WrapperViewModel<TodoListCreateInputVM, TodoListCreateOutputVM> CreateWrapperCreateVM();
	WrapperViewModel<TodoListEditInputVM, TodoListEditOutputVM> CreateWrapperEditVM();
}
