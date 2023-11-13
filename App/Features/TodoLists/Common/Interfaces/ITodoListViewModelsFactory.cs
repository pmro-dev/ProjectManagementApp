using App.Common.ViewModels;
using App.Features.TodoLists.Create;
using App.Features.TodoLists.Delete;
using App.Features.TodoLists.Edit;
using App.Features.TodoLists.Show;

namespace App.Features.TodoLists.Common.Interfaces;

public interface ITodoListViewModelsFactory
{
	TodoListCreateOutputVM CreateCreateOutputVM(string userId);
	TodoListCreateInputVM CreateCreateInputVM(ITodoListDto todoListDto);
	TodoListEditOutputVM CreateEditOutputVM(ITodoListDto todoListDto);
	TodoListEditInputVM CreateEditInputVM(ITodoListDto todoListDto);
	TodoListDeleteOutputVM CreateDeleteOutputVM(ITodoListDto todoListDto);
	TodoListDetailsOutputVM CreateDetailsOutputVM(ITodoListDto todoListDto, DateTime? filterDueDate);
	WrapperViewModel<TodoListCreateInputVM, TodoListCreateOutputVM> CreateWrapperCreateVM();
	WrapperViewModel<TodoListEditInputVM, TodoListEditOutputVM> CreateWrapperEditVM();
}
