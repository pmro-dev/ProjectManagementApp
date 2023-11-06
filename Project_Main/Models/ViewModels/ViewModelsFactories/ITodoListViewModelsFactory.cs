using Project_Main.Infrastructure.DTOs.Entities;
using Project_Main.Models.ViewModels.InputModels;
using Project_Main.Models.ViewModels.OutputModels;
using Project_Main.Models.ViewModels.WrapperModels;

namespace Project_Main.Models.ViewModels.ViewModelsFactories
{
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
}
