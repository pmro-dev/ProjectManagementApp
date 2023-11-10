using Application.DTOs.Entities;
using Web.ViewModels.Inputs;
using Web.ViewModels.Outputs;
using Web.ViewModels.Wrappers;

namespace Web.Factories.ViewModels;

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
